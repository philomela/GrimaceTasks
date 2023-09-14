# GrimaceTasks
### About project

<p align="right">
  <img src="/Docs/GrimaceCoin.png" width="900">
</p>

<b>Grimace coin</b> - a token, a coin that is sold on exchanges, the grimace team regularly issues tasks, upon completion of which coins in grimaces are added.
<br/><b>Grimace tasks application</b> is a parser application that parses information from social networks such as instagram, twitter, etc, and checks that the task conditions are met.

### Architecture

##### Clean architecture

<img src="/Docs/OnionArchitecture.png" width="1100">

Clean Architecture is a software design concept that enforces separation of concerns and respects the preservation of SOLID for ease of maintenance, scalability, and cost reduction depending on platformers or libraries.

##### Sequence diagram

<img src="/Docs/SequenceDiagram.png" width="1100">

<ul>
  <li>Step 1. We receive data on tasks and accounts from the bot's API, using get requests</li>
  <li>Step 2. We save or update the data based on the received data in the database</li>
  <li>Step 3. Parse data for Instagram and check the completion of tasks using the Instagram api</li>
  <li>Step 4. Save the parsing results to the database</li>
  <li>Step 5. Send the results back to the web api of the telegram bot</li>
</ul>

### Sponsors
GrimaceCoin: <a href="https://grimacedoge.com">WebSite</a> 
<a href="https://t.me/grimace">Telegram</a> 
<a href="https://instagram.com/grimace_doge_coin">Instagram</a> 