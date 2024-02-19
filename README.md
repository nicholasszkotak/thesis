# 1. Running the App

This was built using the .NET Core React/WebAPI template. To build and run, open a terminal to the root directory of the project and run `dotnet watch` . This should build the app and launch the API and an SPA Proxy server.

# 2. Description of Project/Architecture

Frontend
========

The frontend is a React application based on the initial .NET Core template. The `ClientApp/src/components` directory contains any visualization components of the application. I try to build my components to be small and pure functions of props, which make them very easy to maintain. For state I used basic React state and passed it between parent and child properties when needed; more complex apps would call for more sophistication, such as Context API or Redux. There is also a `ClientApp/src/services` directory which contains a service class for interacting with the backend.
 
Backend
=======

I used .NET Core WebAPI to build a REST API for fetching information about the computers. It is a single controller which supports basic CRUD applications. I architected the solution roughly following a separation of concerns pattern I used in previous roles which attempts to isolate layers between controller (request/response handling, caching, security, etc.), business (data manipulation, object modeling, etc.), and data access (any database interactions).

Database
=======

SQL Express was used to handle the database. The `db/RebuildDatabase.sql` script creates the database and inserts static data. In the code, I opted to use Dapper ORM to connect to the database, which is a tool I used at previous roles when building microservices and attmepting to reduce the ORM footprint.

# 3. Thoughts

This exercise left a lot of leeway in terms of design, and I found there were a lot of directions I could have taken it. In the interest of limiting time spent on the exercise, I left a lot on the table in terms of missing features, bugginess, and questionable/consistent design choices. In this section, I would like to discuss some of my thoughts regarding my solution and its gaps, and would be happy to implement or discuss any of these items further.

General
======

1) I opted not to implement any validation logic in the interest of time. Typically all fields would be fully validated on the FE before form submission and on the backend when binding to the model.

2) I also did not implement any sophisticated error handling. Typically I try to link backend errors to meaningful HTTP response codes instead of letting the server throw a 500 error. On the frontend, I typically will have error flows for any asynchronous operation that is tied into the desired UX.

3) UX as a whole was not a focus of my solution. Styles and UX functionality are often lacking or not implemented - for example, disabling the Save button while an edit is in progress.

4) When doing write operations, I opted to simply refresh the entire grid instead of trying to update the single row seamlessly. This is because of the low amount of data - in a production system I would likely implement this in a more performant fashion.

5) I ran out of time fully implementing the search mechanism, so searching for ports is not implemented. I would need to update the search DAO method to take in search parameters for the ports and have those ports return through the query.

6) I did not write any tests for this solution due to time constraints. Typically I lean heavy on unit tests following an Arrange-Act-Assert pattern, as well as a smaller set of integration/E2E tests for common flows. A focus on unit testing helps keep a bead on clean, flexible architectures. When a system is built to such an architecture, contracts between units are so well-determined and functionality so consistent that there is less of a need for integration tests on basic functionality. Those tests instead can focus on cross-cutting concerns.

Frontend
======

1) When designing components I try to keep their concerns isolated to only view concerns and state management. As a result, I attempted to split off any API querying into a separate "services" class which is optionally injectable through props. Injecting dependencies this way allows for easier testing; otherwise dependencies need to be mocked through module mocking, which can cause issues with test setup/teardown influencing other tests.

2) Typically I like to approach the frontend as assuming as little as possible from the backend. This is mostly because there may not be control over the backend API. However, if the backend is under full control and operating more as a BFF, then I will sometimes blur the lines a bit for the sake of simplifying frontend logic. An example of this is in trying to "backfill" the list of ports for each computer so that there is always a "zero" row for each port type. Originally I had started implementing that in the frontend but was running into challenges, and found it easier to implement in the backend.

3) I opted to implement search capabilities through the backend, although with the data size for this exercise, a client-side search mechanisms I think would have worked well - that is, filtering the list of edited computers and re-rendering components based on that filtered list. For smaller data sizes this may be a more seamless user experience.

Backend
======

1) I used a multi-layered approach to the backed which splits logic into controller, business, and data access layers. I like this pattern but it was definitely a bit heavy handed for this exercise given the simplicity of the domain. This is evident by boilerplate code when passing between layers. Nonetheless, I like to apply this pattern despite the boilerplate because it encourages me and my teams to think about the overall aspects of a thread of execution, and what kinds of code belongs where. I am less interested with being 100% accurate, as it is always arguable, and more interested in the kinds of conversations that it stimulates. For example, I have some code in ComputerDao when putting together search terms that I think could be better-placed in the controller logic.

2) I lean heavily on dependency injection when designing backends. This is largely to support flexibility in testing but also makes it incredibly easy to support the Strategy pattern, which is super helpful in CI/CD for supporting feature flags, backward compatibility, beta testing, etc.

Database/Data Access
======

1) The initial spreadsheet of data contains values with different units (e.g. RAM has GB and MB, Weight has kg and lb, etc.). I opted to manually convert these to a single unit when creating static data to simplify the system. Typically I like to keep the database simple and leave any data conversions to the consumer.

2) For string fields like Processor and Graphics Card, I stored the whole text string. A more sophisticated engine would likely benefit from splitting apart some of the aspects of these fields such as the brand and model for a richer search and analytic experience.

3) I opted to store the list of Ports as a one-to-many relationship with computers. One tricky aspect was what to do when there wasn't a row for a specific port type. I opted to design for this in the backend but one idea I had was to always write rows with a quantity of 0 for each type, which would simplify a lot of the business logic. Often this isn't a choice - consumers have to work with what they are given - but with full control over the system and full knowledge of which types of ports I would support, this could have been an easier option.

4) Using a more sophisticated ORM like EntityFramework would probably have made my solution more straightforward, especially as it pertains to handling relationships. With Dapper, I had to do a little bit more by hand, which complicates the data access code a bit. In the past, I've always felt like the complexity of ORMs gets in the way and like to design systems where I can opt into their use if the context makes sense, but for this exercise the domain was simple enough where I think it could have helped.

5) When saving ports, I went for a strategy of deleting all rows and re-inserting them. This avoids a lot of complex logic in determining which rows to update and when. This works primarily because of the low record size but may not be feasible for bigger updates, or when auditing on child rows needs to exist.

6) When fetching the list of computers, I simply get all the computers and ports as separate result sets, and stitch them together in the business layer. This likely could have been done via the ORM by doing a JOIN query or having the ORM automatically handle the fetching of child rows, but I wanted to simplify the logic a bit. 

Other
=====

I enjoyed doing this project. It was straightforward but also vague enough to take my mind to a few different places. As a manager, I tend to lean less on my coding abilities and more of my conceptual understandings of software engineering as a whole. For example, in this exercise I was a lot less interested in making the code "perfect" and more interested in thinking about the pros and cons of my solution, the different design patterns used, and how they contributed to an overall system. Those thoughts can be brought to the team and used to drive growth of both the system and the engineers.