**_Disclaimer: I'm still just learning and some of this might be wrong._**

## Architecture Layers

#### Application Core

##### Domain
The domain project is the root project -- i.e., it has no dependencies. This is where the business logic lives. Business-specific entities are defined here (for example, Product).

##### Application
The application project builds off of the domain layer. This is where you can define what kinds of commands, queries, responses, etc. that the application will support, and how those will be executed. There can also be models here that define db tables but are not "core" to the business logic. For example, maybe your app will have a chat feature and you want to store chat history in the application database. This feature is application-specific and not business specific, so the chat models would be defined in the application layer. The DbContext is not implemented here, but the interface which defines it is defined in this layer (unless you're wanting to abstract that futher but for now that's as far as I'm going to go with that).

Other things that might be in this layer:
- Mappers (Automapper) -- map to/from commands to entities
- Validation (FluentValidation)

#### Application Infrastructure

##### Persistence
Adds specific implementations of application requirements. This is where the DbContext lives. The DbContext may contain entities from the Domain layer as well as models from the application layer. You may want to add a repository pattern here that abstracts the DbContext -- this could be helpful if you want to implement things like caching here.

#### Application Presentation
The UI. May have a WebAPI and a client-side web project for example.

#### Shared
Constants, idk