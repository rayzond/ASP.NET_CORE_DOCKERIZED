# fmr_homeproject


You can view the Architecture Diagram in the following link:
https://www.mermaidchart.com/app/projects/465b1188-9ffa-4b4e-8c78-512b514f3581/diagrams/341a87e3-16ae-4832-9ffe-a0d33e47f78b/version/v0.1/edit


📌 System Components Overview

Each component plays a specific role in handling airline data and notifying users.

1️⃣ Event System (RabbitMQ/Kafka)

* A message queue that enables communication between microservices.
* Ensures loose coupling by allowing services to publish and consume events asynchronously.

2️⃣ Relational Database (MSSQL/PostgreSQL)

* Stores user data, user alerts, and pulled airline price data.
* Used by different microservices for data persistence and retrieval.

3️⃣ Scheduler (Microservice)

* Inserts "PullSystemData" events into the Event Queue.
* Runs on a time-based schedule (e.g., every hour) to trigger data pulling.

4️⃣ Puller (Microservice)

* Listens for "PullSystemData" events from the Event Queue.
* Calls airline and price compare websites APIs to fetch updated flight prices.
* Stores the new flight data in the database.
* Emits "CheckUserAlerts" events for every location included in "From" and "To" flight destinations updated.

5️⃣ AlertChecker (Microservice)

* Listens for "CheckUserAlerts" events.
* Compares new prices with users' alerts in the database.
* If a flight price is within a user's preferred range, it creates an "AlertUser" event.

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
