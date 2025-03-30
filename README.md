# fmr_homeproject


You can view the Architecture Diagram in the following link:
https://www.mermaidchart.com/app/projects/465b1188-9ffa-4b4e-8c78-512b514f3581/diagrams/341a87e3-16ae-4832-9ffe-a0d33e47f78b/version/v0.1/edit


📌 System Components Overview

1️⃣ Event System (RabbitMQ/Kafka)

* A message queue that enables communication between microservices.
* Ensures loose coupling by allowing services to publish and consume events asynchronously.

2️⃣ Relational Database (MSSQL/PostgreSQL)

* Stores user data, user alerts, and pulled airline price data.
* Used by different microservices for data persistence and retrieval.

3️⃣ Scheduler (Microservice)

* Inserts "PullSystemData" events into the Event Queue.
* Runs on a time-based schedule (e.g., every 30 seconds) to trigger data pulling for relevant APIs.

  Data Structers:
  
      AggregatorMeta - defines for each API call to collect data the relevant data required:
        * UID - some unique ID so we could compare delta of data.
        * URI - endpoint to call to.
        * Interval - how many minutes between each API call to that service

4️⃣ Puller (Microservice)

* Listens for "PullSystemData" events from the Event Queue.
* Calls airline and price compare websites APIs to fetch updated flight prices.
* Stores the new/updated flight data in the database.
* Emits "CheckUserAlerts" events for every location included in "From" and "To" flight destinations updated.

  Data Structres:  
  
       FlightData - Holds data for each flight:
          * Date - Date of Flight
          * Time - Time of Flight
          * FlightCompany - Company that manages the flight.
          * FlightNumber - Number of flight
          * DepartingFrom - Where the flight is departing from.
          * ArrivingAt - Where the flight is arriving to.
          * Connections - collection of connections for the flight.
          * Seats - collection of SeatData.
          * FlightStatus - hold the status of flights (we should probably only offer alerts regarding flights that are either OnSchedule or Delayed)
          * Airplane model - model of used airplane.
  
      SeatData - Holds data for each seat in a flight.
          * Number - number of seat (usually following by a letter)
          * Price - price to pay for the seat (basic price without additional ammeneties)
          * AvailableAmmeneties - ammeneties that are available to this seat spot.
          * Class - specified class for this seat, usually determines price and different available ammeneties. (Economy/Buisness/etc.).
  

5️⃣ AlertChecker (Microservice)

* Listens for "CheckUserAlerts" events.
* Compares new prices with users' alerts in the database.  
* If a flight price is within a user's preferred range, and connections for flight isn't above what the user set to, it creates an "AlertUser" event.

  Data Structures:

      UserAlert - holds data for a specific user defined alert for a From and To combination.
        * From - where the flight departs from.
        * To - where the flight arrives to.
        * MaxPrice - maximum price the user wants to see flight tickets for this specific flight.
        * MaxConnections - maximum connections the user accepts for this flight.
    

6️⃣ AlertNotifier (Microservice)

* Listens for "NotifyUser" events.
* Sends notifications to users via Firebase (Android) or APNs (iPhone).
* Ensures users receive timely alerts about price drops.

📌 Event Definitions

These are messages exchanged between components.

🔹 "PullSystemData" Event

    Created by Scheduler and consumed by Puller.
    Triggers a data fetch from external airline APIs.

🔹 "CheckUserAlerts" Event

    Created by Puller and consumed by AlertChecker.
    Indicates that new airline data is available for validation against user alerts.

🔹 "NotifyUser" Event

    Created by AlertChecker and consumed by AlertNotifier.
    Notifies the system that a user should be alerted about a price drop.

System Improvement Suggestions:

* Add logging throughout the system for failures, warnings, and debug information.
* Add Unit testing framework and test for the various business logic at a unit level.
* In case the system provide services to big numbers of users and has heavy usage:
    * Add ElasticSearch and use it to search for the relevant UserAlert when triggering NotifyUser
    * Add caching to basic required user information (like country, used currency)
