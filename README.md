# Social Application

## Introduction
The SocialApiv3 application backend is responsible for managing users, and other related functionalities. It provides a RESTful API for frontend applications to interact with.This documentation  API endpoints, authentication and authorization mechanisms.


# API Endpoints
The following sections describe the available API endpoints categorized by functionality.

## Users
- ​GET      **/api​/Users:** Retrieve a list of all users.
- GET  ​    **/api​/Users​/{id}:** To get details of specific user
- PUT ​     **/api​/Users​/{id}:** To alter specific details of specific user
- POST     **/api/Users/add-photo/:**  To add user photos
- GET      **/api/Users/setMain/{photoid}:** to make a photo the main so it appear as the profile photo
- DELETE   **/api/Users/DeletePhoto/{photoid}:** to delete a specific phot 
  
## Likes
- GET /api/Likes/{id}:to like a user
- GET /api/Likes/GetLikes/{p} it retrieves users who liked you or the user you liked based on 
                              the prameter {p}
## Messages 
- POST /createMessage    create new message 
- GET  /messages         Retrieve inbox,outbox and unread based on parameters (inbox,outbox,unread)
- GET /thread/{userId}   Retrieve a conversation between two user  
- DELETE  /message/delete/{id}  delete a specific message 

## Authentication
- GET /api/Auth/Register: To Signup (create user).
- GET /api/Auth/GetToken: Retrieve the token of the user.

## Authorization
Authorization is handled through JWT-based access tokens. Include the access token in the Authorization header of each API request as a Bearer token.

# Technologies
- Asp .net core 
- Entity Framework core 
- SignalR
- ASP.NET Identity





  

