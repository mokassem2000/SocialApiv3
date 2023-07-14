# Social Application

## Introduction
The social application backend is responsible for managing users, and other related functionalities. It provides a RESTful API for frontend applications to interact with.
This documentation outlines the setup, configuration, API endpoints, authentication and authorization mechanisms, testing, deployment, and contributing guidelines.


# API Endpoints
The following sections describe the available API endpoints categorized by functionality.

## Users
- ​GET      **/api​/Users:** Retrieve a list of all users.
- GET  ​    **/api​/Users​/{id}:** To get details of specific user
- PUT ​     **/api​/Users​/{id}:** To alter specific details of specific user
- POST     **/api/Users/add-photo/:**  To add user photos
- GET      **/api/Users/setMain/{photoid}:** to make a photo the main so it appear as the profile photo
- DELETE   **/api/Users/DeletePhoto/{photoid}:** to delete a specific phot 
  
## Authentication
- GET /api/Auth/Register: To Signup (create user).
- GET /api/Auth/GetToken: Retrieve the token of the user.

## Likes
- GET /api/Likes/{id}:to like a user
- GET /api/Likes/GetLikes/{p} it retrieves users who liked you or the user you liked based on 
                              the prameter {p} 

## Messages
- POST /createMessage
- GET  /messages
- GET /thread/{userId}
- DELETE  /message/delete/{id}
