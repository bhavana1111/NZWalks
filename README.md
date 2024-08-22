# NZWalks

The code inside the program.cs is executed first
ASP.net gives dependency injection by default
<br/>
REST emphasis stateless client server model.
This means that the server should not store any client state between these requests
<br/>

# DTO

Data Transfer Objects

Used to transfer data between different layers
typically contain a subset of the properties in the domain model
for example transferring data over a network

# Advantages of DTO

1.Some data which we do not want to show to our clients can be hidden
2.Seperation of concerns: The domain objects are usually tightly coupled with a database schema and we just know, because Entity Framework core uses them to talk to a Database, Whereas DTO's an be designed to match the business requirements
3.performance:allows us to retrieve the data which is only needed
4.Security:DTO can help us to improve the security by exposing only required data reduces the risk of exposing sensitive data to unauthorised users.
5.versioning:We can change the domain modle but keep the DDTO the same so that we have not breaking the contract between the client and API

# Repository Pattern

1.Design pattern to separate the data access layer from the application
2.Provides interface without exposing implementation
3.Helps create abstraction
