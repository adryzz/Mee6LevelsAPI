# Mee6LevelsAPI
Mee6 Levels API Wrapper for .NET

Uses [Newtonsoft.Json](https://www.newtonsoft.com/json) as a reference.

How to retrieve user info:
```C#
Mee6UserInfo info = Mee6.GetUserInfo(1000, 2000);//these are guild ID and user ID. change them
Console.WriteLine($"The user {user.username} is at level {info.level}.");
```

if you need to retrieve multiple users' info:
```C#
Mee6UserInfo[] info = Mee6.GetServer(1000).players;//this is guild ID
```
By default, this library will search up to 1000 users. if you want to load more than 1000 users, do this.

```C#
Mee6.Limit = 10000;//It will load now 10000 members
```

The library is pretty straightforward to use, and has a JSON-like structure.
