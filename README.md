# Social Application

## Introduction
The social application backend is responsible for managing users, and other related functionalities. It provides a RESTful API for frontend applications to interact with.
This documentation outlines the setup, configuration, API endpoints, authentication and authorization mechanisms, testing, deployment, and contributing guidelines.


# API Endpoints
The following sections describe the available API endpoints categorized by functionality.

## Messages
- ​GET  /api​/Users:Retrieve a list of all users.
- GET  ​/api​/Users​/{id}
- PUT ​ /api​/Users​/{id}
- POST /api/Users/add-photo/
- GET  /api/Users/setMain/{photoid}
- DELETE  /api/Users/DeletePhoto/{photoid}
  
## Auth
- GET /api/Auth/Register: To Signup (create user).
- GET /api/Auth/GetToken: Retrieve the token of the user.

## Likes
- GET /api/Likes/{id}:to like a user
- GET /api/Likes/GetLikes/{p}

