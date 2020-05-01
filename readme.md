# Angular & .NET Core 3.1 Sample Project
This repository contains a project that is intended as a starting point for a web-application based on a .NET Core 3.1 API and Angular based front-end.

Authenticaiton is provided by IdentityServer4.

Database abstraction is provided by EF Core 3.1.

#### Getting Started
To run the project run `npm start` in the `.\ClientApp` folder and `dotnet run` in the root folder - in that order. This will allow Angular development in front-end or server without having to rebuild the other.

#### Configuration
##### Identity Server
IdentityServer has discrete settings for development and production configuration. In a development config, IS4 will use the local certificate file `auth_key_dev.pfx`. In production, IS4 will look for the same certificate in the cert store on the deployed machine.



