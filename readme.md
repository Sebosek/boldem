# Boldem Console App

The solution is made by several projects. 
The first project is `Boldem.XmlGenerator`, it's a simple console app used to generate a sample XML file. 
The second project is `Boldem.ConsoleApp` which is the main application; takes already generated XML file and imports contacts from this file using the Boldem API. 
Finally, the third project is just a xUnit Test project.

## How to run it
To be able to run `Boldem.ConsoleApp` you have to define `BOLDEM_ID` and `BOLDEM_SECRET` environment variables.
These variables are used as OAuth credentials to get a bearer token which is later used to call the API. 
The value of `BOLDEM_ID` is a Client ID and `BOLDEM_SECRET` is a client secret.

## What can be improved
There are multiple areas where the project could be improved, namely:

- Using dependency injection container; intentionally the application doesn't use any DI container. The reason for that is simply because the app is too small and simple, however following the SOLID principles.
- OAuth service, the current implementation doesn't refresh the token. We import only 3000 contacts which can be easily imported in a couple hundred of milliseconds.
- Add rate limiting, again, if we would upload a hundred times more contacts we would probably need to add a rate limit to not overflow the API [link](https://learn.microsoft.com/en-us/dotnet/core/extensions/http-ratelimiter).
- HTTP Client factory, no more explanations are needed if you know what I am talking about.
- Consider using a delegate handler to attach a Bearer token, however, it has its own pros and cons.
- Domain modeling, currently we basically have no domain modeling, the candidate is eq. `JWToken`.
- Better UI, the app currently has basically no UI.
- Better UI abstraction, in the current implementation we're directly using the `Console` static class.
- Because the current app is basically a utility app, there is no architecture, we only use N layered architecture style.

As already mentioned, during the development I've followed next to the SOLID principles, YAGNI, and the Pareto principle.

## Final words
If you've found this repository "by mistake" and you apply or going to apply for a developer position in Boldem, try to come up with your own solution.
My solution the most definitely sucks... 😄
