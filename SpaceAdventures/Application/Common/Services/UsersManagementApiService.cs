﻿using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpaceAdventures.Application.Common.Commands.Users;
using SpaceAdventures.Application.Common.Exceptions;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;
using SpaceAdventures.Application.Common.Queries.Users.Queries;
using SpaceAdventures.Application.Common.Services.Interfaces;
using SpaceAdventures.Domain.Entities;



namespace SpaceAdventures.Application.Common.Services;

public class UsersManagementApiService : IUsersManagementApiService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly ISpaceAdventureDbContext _context;

    #region Constructor

    public UsersManagementApiService(IConfiguration configuration, HttpClient httpClient, IMapper mapper, ISpaceAdventureDbContext context)
    {
        _configuration = configuration;
        _mapper = mapper;
        _context = context;
        _httpClient = httpClient;
    }

    #endregion

    #region Get All Users
    public async Task<UsersVm> GetAllUsers(CancellationToken cancellationToken)
    {
        return new UsersVm
        {
            UsersList = await _context.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .OrderBy(u => u.IdUser)
                .ToListAsync(cancellationToken)
        };
    }
    #endregion

    #region Get User's Roles

    public async Task<List<UserRole>> GetUserRoles(string userId, CancellationToken cancellation)
    {
        var tokenResponse = await GetToken();
        var tokenAccess = tokenResponse.access_token;

        var url = _configuration["Auth0ManagementApi:Audience"] + "users/" + userId + "/roles";

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAccess);

        var httpResponse = await _httpClient.GetAsync(url, cancellation);

        if (!httpResponse.IsSuccessStatusCode) throw new NotFoundException("Users", userId);

        var content = await httpResponse.Content.ReadAsStringAsync(cancellation);
        var roles = JsonConvert.DeserializeObject<List<UserRole>>(content);
        return roles;
    }

    #endregion

    #region Get All Auth0 Roles
    public async Task<List<UserRole>> GetAllRoles(CancellationToken cancellationToken)
    {
        var tokenResponse = await GetToken();
        var tokenAccess = tokenResponse.access_token;
        var url = _configuration["Auth0ManagementApi:Audience"] + "roles";

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAccess);
        var httpResponse = await _httpClient.GetAsync(url, cancellationToken);
        if (!httpResponse.IsSuccessStatusCode) throw new NotFoundException();

        var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        var roles = JsonConvert.DeserializeObject<List<UserRole>>(content);
        return roles;

    }
    #endregion

    #region Collecting Access Token

    public async Task<TokenData> GetToken()
    {
        var response =
            await _httpClient.PostAsync(_configuration["Auth0ManagementApi:Path"], new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    {"client_id", _configuration["Auth0ManagementApi:ClientId"]},
                    {"grant_type", _configuration["Auth0ManagementApi:GrantType"]},
                    {"client_secret", _configuration["Auth0ManagementApi:ClientSecret"]},
                    {"audience", _configuration["Auth0ManagementApi:Audience"]}
                }));

        if (!response.IsSuccessStatusCode) throw new NotFoundException("Unable to retrieve access token");

        var content = await response.Content.ReadAsStringAsync();
        var tokenData = JsonConvert.DeserializeObject<TokenData>(content);
        return tokenData;
    }
    #endregion

    #region CreateUser
    public async Task<UserDto> CreateUser(UserInput userInput,CancellationToken cancellationToken)
    {
        try
        {
            User user = await CreateUserAuth0(userInput, cancellationToken);
            bool ok = await AssignRole(user, cancellationToken);
            return await CreateUserInDb(user,cancellationToken);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }


    public async Task<bool> UserExists(string email)
    {
        return await _context.Users.AnyAsync(c => c.Email==email);
    }


    public async Task<User> CreateUserAuth0(UserInput userInput, CancellationToken cancellationToken)
    {
        var token = await GetToken();
        var accessToken = token.access_token;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.PostAsync(_configuration["Auth0ManagementApi:Audience"] + "users",new FormUrlEncodedContent(
            new Dictionary<string, string>
            {
                {"email", userInput.Email},
                {"email_verified", "false"},
                {"connection", "Username-Password-Authentication"},
                {"verify_email","true"},
                {"given_name", "John"},
                {"family_name", "Doe"},
                {"name", "John Doe"},
                {"nickname", "Johnny"},
                {"password", "Test1234**/"},
            }), cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new ValidationException();
        }
        
        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        var userAuth = JsonConvert.DeserializeObject<UserAuth0>(content);

        var userDb = _mapper.Map<User>(userAuth);
        userDb.IdRole = userInput.IdRole;
        userDb.Username = userInput.Username;

        return userDb;
    }

    public async Task<UserDto> CreateUserInDb(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UserDto>(user);
    }
    #endregion

    #region Assign Role to a user
    public async Task<bool> AssignRole(User user,CancellationToken cancellation) // ou directement UserId
    {

        // AccessToken AuthManagement API
        var token = await GetToken();
        var accessToken = token.access_token;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        List<UserRole> lst = await GetAllRoles(cancellation);
        Role role = await GetRoleInDb(user, cancellation);

        var result = lst.Find(c => c.name == role.Name);

        string[] tab = new string[1];
        tab[0] = user.IdUserAuth0;
        string testUserAuth = user.IdUserAuth0;
        string JsonToPost = "users\":[\""+testUserAuth+"\"]";
        //var value = new List<KeyValuePair<string,string[]>>();
        //value.Add(new KeyValuePair<string, string[]>("users",tab));
        //var content = new FormUrlEncodedContent(value);
        //var test = new FormUrlEncodedContent(new Dictionary<string, string>
        //{
        //    { "users", JsonToPost }
        //});

        //var json = JsonConvert.SerializeObject(tab);

        UserTempData users = new UserTempData();
        users.users = tab;
        var json = JsonConvert.SerializeObject(users);

        //var response = await _httpClient.PostAsync(_configuration["Auth0ManagementApi:Audience"] + result.id + "/users",
            
        //    new StringContent(json, Encoding.UTF8, "application/json"));

        var response = await _httpClient.PostAsJsonAsync(_configuration["Auth0ManagementApi:Audience"] + result.id + "/users",

            users);

        // var response = await _httpClient.PostAsync()

        // Endpoint /api/v2/roles/{id}/users

        return true;
    }
    #endregion

    #region Get Role By IdRole
    public async Task<RoleDto> GetRoleInDbByIdRole(int id, CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null) throw new NotFoundException(nameof(Role), id);

        return _mapper.Map<RoleDto>(role);

    }
    #endregion

    #region GetRole from DB
    public async Task<Role> GetRoleInDb(User user, CancellationToken cancellation)
    {
        try
        {
            var userRole = _context.Roles.SingleOrDefault(c => c.IdRole == user.IdRole);
            return userRole;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion


    public async Task AssignRole(string id, AssignRolesRequest request, CancellationToken cancellationToken = default)  
    {
        var tokenResponse = await GetToken();
        var tokenAccess = tokenResponse.access_token;

        var url = _configuration["Auth0ManagementApi:Audience"] + "users/auth0|" + id + "/roles";

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAccess);

        var payload = JsonConvert.SerializeObject(request);

        var response = await _httpClient.PostAsync(url, new StringContent(payload, Encoding.UTF8, "application/json"), cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new ValidationException();
        }
    }

}