# Sum Number
 This is a problem derived from Leet Code:

Given an array of integers, return indices of the two numbers such that they add up to a specific target.

You may assume that each input would have exactly one solution, and you may not use the same element twice.

Example:

Given nums = [2, 7, 11, 15], target = 9,

Because nums[0] + nums[1] = 2 + 7 = 9,
return [0, 1].



# Available APIs
 To get the 2 numbers use this query:
 https://localhost:5001/api/sumnumber?numbers=2,7,11,5&target=9

 The return must be: { 0, 1 }




# To build and test
To run API: `dotnet run -p Api` <br>
To just build: `dotnet build` <br>
To run test: `dotnet test` <br>
To run test verbose + logging: `dotnet test -l:"console;verbosity=detailed"`
