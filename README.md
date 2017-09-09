# VaseLiga
.NET library to fetch and parse results from VaseLiga - a Czech amateur sports site (http://vaseliga.cz)

# Installing & Using

Install from [NuGet](https://www.nuget.org/packages/VaseLiga/) via Visual Studio's package manager console

    PM> install-package VaseLiga

... or using the `dotnet` command:

    $ dotnet add package VaseLiga

# Example

To download the results for a given player you'll need to know:

* the player's id - you can see this in the URL when you have selected that player. It will usually be the name they have entered with a "-" instead of a space. So for "Sean McLemon" this would be *"sean-mclemon"*
* the sport and city you want to get results for, for me this is *Badminton* in *Brno*

Import the `VaseLiga` namespace, initialise a `VaseLigaReader` and invoke the `.Get()` method with the player's ID and the appropriate sport/city ID (which is an integer - more on this later):

            var reader = new VaseligaReader();
            var results = reader.Get("sean-mclemon", BadmintonSingles.Brno);
            foreach (var result in results) 
            {
                Console.WriteLine("{0} - {1}", result.Opponent, result.Result);
            }

# Sport/City IDs

There are helper classes which define the appropriate city/sport ID. They're all of the form `Sport.City`. These are:

* `BadmintonSingles.Brno`
* `BadmintonSingles.HradecKralove`
* `BadmintonSingles.Olomouc`
* `BadmintonSingles.Pardubice`
* `BadmintonSingles.Plzen`
* `BadmintonSingles.Praha`
* `BadmintonDoubles.Brno`
* `BadmintonDoubles.Praha`
* `Squash.Brno`
* `Squash.Plzen`
* `Squash.Praha`
* `BeachVolleyball.Praha`
* `TennisSingles.Brno`
* `TennisSingles.Plzen`
* `TennisSingles.Praha`
* `TennisDoubles.Praha`
* `PingPong.Brno`
* `PingPong.Plzen`
* `PingPong.Praha`