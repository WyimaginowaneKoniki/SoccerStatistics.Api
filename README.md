[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NetCoreVersion: 3.1](https://img.shields.io/static/v1?label=.Net%20Core&message=3.1&color=informational)](https://dotnet.microsoft.com/download/dotnet-core/3.1)
[![Coverage Status](https://coveralls.io/repos/github/WyimaginowaneKoniki/SoccerStatistics.Api/badge.svg?branch=master)](https://coveralls.io/github/WyimaginowaneKoniki/SoccerStatistics.Api?branch=master)
# SoccerStatistics REST API

## Overview

SoccerStatistics is a REST API application to search information about Football leagues,teams,players etc.


## Table of contents
* [API Versioning](#api-versioning)
* [URI Summary](#uri-Summary)
* [HTTP Responses](#HTTP-Responses)
* [HTTP Response Codes](#HTTP-Response-Codes)
* [Technologies](#technologies)
* [Contributors](#Contributors)



## API Versioning
The first part of the URI path specifies the API version you wish to access in the format `v{version_number}`. 

For example, most current version of the API is accessible via:

```no-highlight
http://localhost:{port}/swagger/index.html
```

## URI Summary

The following table summarises all the available resource URIs, and the effect of each verb on them. Each of them is relative to the base URI for our API: `http://localhost:{port}/api`.

| Resource                                              | GET                                                 |
| ----------------------------------------------------- | --------------------------------------------------- | 
| [/leagues/](#leagues)                                 | Returns list of available leagues                   |
| [/leagues/{id}](#leagues)                             | Returns league by ID                                |
| [/matches/](#matches)                                 | Returns list of recent matches                      |
| [/matches/{id}](#matches)                             | Returns match by ID                                 |
| [/players/](#players)                                 | Returns list of available players                   | 
| [/players/{id}](#players)                             | Returns player by ID                                |
| [/rounds/{id}](#rounds)                               | Returns round by ID                                 |                
| [/stadiums/](#stadiums)                               | Returns list of available stadiums                  |
| [/stadiums/{id}](#stadiums)                           | Returns stadium by ID                               |
| [/teams/](#teams)                                     | Returns list of available teams                     | 
| [/teams/{id}](#teams)                                 | Returns team by ID                                  | 


## HTTP Responses
For example, a request to the `/leagues/` resource might return this:

If an error occurred - see [HTTP Response Codes](#HTTP-Response-Codes)).

``` JSON
{
   "Id":1,
   "FullName":"Blick, Nicolas and Mraz Group",
   "ShortName":"Blick, Nicolas and Mraz",
   "CreatedAt":1973,
   "Coach":"Katrina Bergstrom",
   "City":"Berniermouth",
   "Players":
        {"Id":1,
        "Name":"Melyna",
        "Surname":"Klocko",
        "Height":191,
        "Weight":66,
        "Birthday":"1980-12-24T00:00:00",
        "Nationality":"Burundi",
        "DominantLeg":0,
        "Nick":"Klocko_Rowe22",
        "Number":89,
        "Id":2,
        "Name":"Ayla",
        "Surname":"Reichert",
        "Height":181,
        "Weight":58,
        "Birthday":"1980-05-16T00:00:00",
        "Nationality":"Ecuador",
        "DominantLeg":1,
        "Nick":"Reichert_Dare",
        "Number":1},
   "Stadium":
        {"Id":1,
        "Name":"Nitzsche Inc Stadium",
        "Country":"Turks and Caicos Islands",
        "City":"Powlowskibury",
        "BuiltAt":1943,
        "Capacity":30112,
        "FieldSize":"96x06",
        "Cost":362488.25,
        "VipCapacity":5499,
        "IsForDisabled":true,
        "Lighting":51713,
        "Architect":"Lazaro Rogahn",
        "IsNational":true}
  }
```

## HTTP Response Codes
Each response will be returned with one of the following HTTP status codes:

* `200` `OK` The request was successful
* `400` `Bad Request` There was a problem with the request (security, malformed, data validation, etc.)
* `404` `Not found` An attempt was made to access a resource that does not exist in the API
* `500` `Server Error` An error on the server occurred

Sample error response:

``` JSON
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404,
  "traceId": "|953bb372-40d8e72e899583c2."
}
```
# Technologies
* [Asp.Net Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
* [Docker](https://www.docker.com/)
* [AutoMapper](https://automapper.org/)
* [Autofac](https://autofac.org/)
* [MediatR](https://github.com/jbogard/MediatR)
* [EntityFrameworkCore](https://docs.microsoft.com/en-us/ef/core/)
* [MS SQL](https://www.microsoft.com/pl-pl/sql-server/)
* [NLog](https://www.microsoft.com/pl-pl/sql-server/)
* [xUnit](https://xunit.net/)
* [Moq](https://github.com/moq/moq4)
* [Swagger](https://swagger.io/)
* [Github Actions]( https://github.com/features/actions) 
## Contributors

<a href="https://github.com/JustD4nTe"><img src="https://avatars0.githubusercontent.com/u/15444187?s=400&v=4" title="JustD4nTe" width="80" height="80"></a>
<a href="https://github.com/Blazevarjo"><img src="https://avatars1.githubusercontent.com/u/46849151?s=400&v=4" title="Blazevarjo" width="80" height="80"></a>
<a href="https://github.com/PieterQQ"><img src="https://avatars2.githubusercontent.com/u/25612795?s=460&v=4" title="PieterQQ" width="80" height="80"></a>
<a href="https://github.com/KamilDonda"><img src="https://avatars2.githubusercontent.com/u/44376350?s=460&v=4" title="Kamil Donga" width="80" height="80"></a>
