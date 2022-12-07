# Learning ASP.NET Core MVC - Users & Roles

Small example showing how to manage basic authentication (cookie based) and authorization (with roles) in an ASP.NET Core MVC app - without using Identity support.

This sample was created for my own use during lectures, so nothing fancy here :-).

Please remember that to start this app, you have to:

- because migrations contain passwords generated with my "pepper" hash, you might need to perform first: `Remove-Migration`, `Add-Migration InitialMigration`,
- then you can create database by using `Update-Database` (you might need to change the connection string to do that),
- to login in as a cumstomer you have to use username "jdoe" and password "P@ssw0rd".

More information about authentication/authorization in ASP.NET Core MVC can be found here: [ASP.NET Core Authentication and Authorization Tutorials](https://youtube.com/playlist?list=PLOeFnOV9YBa4yaz-uIi5T4ZW3QQGHJQXi).

If you would need some random hashes, check out [this](https://onlinehashtools.com/generate-random-sha256-hash) website.

If you are certain that you want to reainvent the wheel and implement your own authentication (I would argue that it is not such a great idea ;-), and I'm almost sure that Identity framework would do that a lot better), these resources might help you :-):

- [Hashing and Salting Passwords in C# – Best Practices](https://code-maze.com/csharp-hashing-salting-passwords-best-practices/),
- [Store Passwords Securely in Database using SHA256 — ASP .NET Core](https://juldhais.net/secure-way-to-store-passwords-in-database-using-sha256-asp-net-core-898128d1c4ef).
