# Vanguard Framework - DEPRICATED

**NOTE: This project has been depricated and archived. There are other package like MediatR that do a beter job than these package. Please migrate your code to other solutions.**

The Vanguard Framework is a framework for developing database driven web applications and web services. It combines a set of design patterns and best practices to kick start your project.

By using the Vanguard Framework and following its design patterns you ensure that your code is of good quality, can be maintained easily and extended without a lot of hassle.

The Vanguard Framework is based on the following design patterns.

1. Domain Driven Design (DDD)
2. Domain Events
3. Repository Pattern
4. Command Query Responsibility Segregation (CQRS)
5. Inversion of Control (IoC)

## Architecture
The Vanguard Framework is built with a n-tier architecture in mind. The Vanguard Framework takes the following layers into account.

1. Data Access Layer
2. Business Layer
3. Service Layer
4. User Interface Layer

### Data Access Layer
The data access layer is based on the entity framework. It is possible to build it around another framework but this would mean that you have to do some customization.

In the business layer the data/domain entities are located. The data entities can be accessed via repositories and the data entities can raise Domain Events.

### Business Layer
The business layer contains all business logic like commands, queries, validator and event handlers.

### Service Layer
The service layer is based on ASP.NET Web API 2.0 and make functionality available via JSON REST service. The service layer consists of little to no functionality. It passes all request to the business layer. The reason for this is that we want the application logic reside in one location, the business layer. It also makes it easier to change the service layer framework to something else like Windows Communication Foundation (WCF). 

### User Interface Layer
The framework for the user interface layer has not been identified yet because the Vanguard Framework is work in progress. As soon as the functionality for the other layers are finished we will decide on this. For now we are leaning towards AngularJS 4.0 but this is not set in stone.

# Documentation
Take a look at the [Wiki](https://github.com/jgveire/Vanguard.Framework/wiki) for more documentation.
