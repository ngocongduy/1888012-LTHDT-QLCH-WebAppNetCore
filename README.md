# 1888012-LTHDT-QLCH-WebAppNetCore
A basic web app with net core 3.0

Preface: This is my first web app made during learning. Most of the work of my app are originated from the internet, especially thanks to the tutorial for ASP NET CORE of sir Kuvenkat - PragimTech.
I publish it just for personal use during learning. You are free to use if it helps. 

Design architecture: MVC & 3-layer
MVC
- Controller: control routes within the app, performing main works of the app
- View: Razor pages responded to clients. Each view may use one or more ViewModel for DataBinding when clients send requests to the server and vice versa.
- Model: a collection of Model (for general uses) and ViewModel (for View)

3-layer
- User interface (UI): This are plain pages sent by server and rendered by a browser on clients. It performs client-side works (using Javascript and relating library) such as data validation.
If required, it will send requests to server on behalf of the user. Then UI mainly work with Views.
(Upon a client's request, server will pick a corresponding view, do some required works (thanks to a cooperation in M-V-C) then generate a html-page, finally send it to client for rendering.)
- Business layer (BL): This layer is a hidden working environment in the server, the playground for Models, Views and Controllers. It is where main operations done: routing inside the app, authorization, authentications, data queries and so on.
This layer is the intermediate controlling data transactions between clients and databases.
- Data access layer (DL): The layer standing some models to help you interect with databases. In this case is SQL server. You can also define models to work with your local storages.

