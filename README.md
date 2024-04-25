# Asp.Net core Onion Architecture Web API for E-commerce.
This project is a web API for an e-commerce application built using Onion Architecture.

## overview
#### this project is a simple e-commerce web API using onion Architecture with crud operation, I used solid principles  and design patterns as I need.

#### ASP.NET Identity which is a membership system for adding login functionality to web applications. It provides APIs for user authentication, authorization, and user management. ASP.NET Identity allows you to manage users, roles, claims, and external logins, It includes features like user registration, login/logout, password management and account confirmation.

#### JWT is a token-based authentication mechanism. When a user logs in, the server generates a JWT token containing user information (claims) and sends it back to the client.

#### Rate limiting is a strategy used in software development and web services to control the rate of incoming requests from clients or users. It helps to prevent abuse, protect against denial-of-service (DoS) attacks, ensure fair usage of resources, and maintain system stability and performance.

#### Dependency Injection (DI) is a design pattern and a technique used in software engineering, particularly in object-oriented programming, to achieve loose coupling between components and to improve code maintainability, testability, and scalability.

 ## Onion Architecture Layers:
 ### Domain Entities Layer 
It is the center part of the architecture. It holds all application domain objects.
If an application is developed with the ORM entity framework then this layer holds POCO classes (Code First) or
Edmx (Database First) with entities. These domain entities don't have any dependencies.
 
### Repository Layer
The layer is intended to create an abstraction layer between the Domain entities layer and the Business Logic layer of an application.
It is a data access pattern that prompts a more loosely coupled approach to data access. We create a generic repository,
which queries the data source for the data, maps the data from the data source to a business entity, and persists changes in the business entity to the data source.
 
### Service Layer
The layer holds interfaces which are used to communicate between the UI layer and repository layer.
It holds business logic for an entity so it’s called the business logic layer as well.
 
### UI Layer
It’s the most external layer. It could be the web application, Web API, or Unit Test project.
This layer has an implementation of the Dependency Inversion Principle so that the application builds a loosely coupled application.
It communicates to the internal layer via interfaces.


