# Mee6LevelsAPI
Mee6 Levels API Wrapper for .NET

Uses [Newtonsoft.Json](https://www.newtonsoft.com/json) as a reference.

How to retrieve user info:
```C#
Mee6UserInfo info = Mee6.GetUserInfo(105753365916422144, 509442538378100736);//these are guild ID and user ID. change them
Console.WriteLine($"The user {user.Username} is at level {info.Level}.");
```

if you need to retrieve multiple users' info:
```C#
Mee6UserInfo[] info = Mee6.GetServer(105753365916422144).Users;//this is guild ID
```
By default, this library will search up to 1000 users. if you want to load less than 1000 users, do this. the public API can access up to 1000 members, so there's that.

**Edit: they either changed it or something, but i am able to load more than 1000 users on a premium server using the public API.**

```C#
Mee6.Limit = 500;//It will load now 500 members
```

The library is pretty straightforward to use, and has a JSON-like structure.
