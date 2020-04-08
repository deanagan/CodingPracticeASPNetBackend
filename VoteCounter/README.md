# Sum Number
 This is a vote counter web service.
This will have a rate limiter feature.


# Available APIs
 To get the 2 numbers use this query:
 https://localhost:5001/api/sumnumber?numbers=2,7,11,5&target=9

 The return must be: { 0, 1 }




# To build and test
To run API: `dotnet run -p Api` <br>
To just build: `dotnet build` <br>
To run test: `dotnet test` <br>
To run test verbose + logging: `dotnet test -l:"console;verbosity=detailed"`
