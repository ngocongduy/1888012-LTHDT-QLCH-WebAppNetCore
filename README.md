# 1888012-LTHDT-QLCH-WebAppNetCore
A basic web app with net core 3.0

### Preface: 
This is my first web app made during learning. Most of the work of my app are originated from the internet, especially thanks to the tutorial for ASP.NET CORE of sir Kudvenkat - PragimTech. I publish the app just for personal use, howerver, you feel free to try and hope it helps.
**Information you infer from my work shall be used as a source of reference and not guaranteed correct.**
:monkey_face:

### Design architecture: MVC & 3-layer
#### I. MVC
- Controller: control routes within the app, performing main works of the app
- View: Razor pages responded to clients. Each view may use one or more ViewModel for DataBinding when clients send requests to the server and vice versa.
- Model: a collection of Model (for general uses) and ViewModel (for View)

#### II. 3-layer
- Presentation layer (PL): Here containing plain pages generated and sent by server then they are rendered by a browser on clients. They perform client-side works (using Javascript and relating library) such as data validation. If required, they will send requests to server on behalf of the user. Hence PL mainly work withs Views.
(Upon a client's request, server will pick a corresponding view, do some required works (thanks to the cooperation of M-V-C) then generate a html-page, finally send it to client for rendering.)
- Business layer (BL): This layer is the playground for Models, Views and Controllers. It is where main operations done: routing inside the app, authorization, authentications, data queries and so on. This layer is the intermediate accounted for controlling data transactions between clients and databases.
- Data access layer (DL): The layer standing some models to help you interect with many kinds of data storage. In this case is SQL server. You can also define models to work with your local storages.


### App description: This web app is a very simple Store Management which can help the users (store staff) track products in the store.
1. __View (Presentation layer)__
  	1. Login (default route) & Register: In order to join, you must register for an account and log in
	1. Home
		+ Homepage: Index page give you 2 options for generating reports: StockByType & OutOfDate
		+ Report for stocks grouped in types: StockByType
		+ Report for expired stocks: OutOfDate
	1. Product: you can add/ update/ delete/ and search for product type and product by routing among this category
		+ For product: Detail & Search
		+ For product type: ProductType & SearchType
	1. Stock: you can add/ update/ delete/ and search for records of products in and out the Store
		+ Track stock in: StockIn
		+ Track stock out: StockOut 
1. __Model & ViewModel (Business layer and Presentation layer)__
	1. Model:
		+ Class for product: Product
		+ Class for product: ProductType
		+ Class for tracking stock: StockTrackDetail
		+ Interface: IProductRepositor *(For Constructor Dependency Injection - Only for ProductController)*
		+ Implementation for IProductRepository: MockProductRepository *(For mock test)*
		+ Class for database access: AppDbContext
		+ Class for Authorization service: ClaimStore & UserClaim
		+ Class to customize built-in IdentityUser: ApplicationUser *(Currently not used)*
		+ Class for Error views: ErrorView
	1. ViewModel: intermediate models for views to interact/ databinding with controller easier
		+ Product views: ProductViewModel
		+ ProductType views: ProductTypeViewModel
		+ StockIn views: StockInViewModel
		+ StockOut views: StockOutViewModel
		+ and so on...
1. __Controller (Business layer)__
	1. Register new account and login control: AccountController
	1. User role/claim management: AdministrationController
	1. Product and produc type management: ProductController 
	1. Stock tracking: StockController
	1. Home page and reports: HomeController
	1. To log and render error page (error handling): ErrorController *(currently not used)*

1. __Data access layer (DAL)__
	1. To read and write files in folder root/data
		+ Product data: product.json
		+ Product type data: product_type.json
		+ Stock in records: stock_in.json
		+ Stock out records: stock_out.json
	1. Migrations folder: tracking records during migration/update database scheme - code-first approach
	
### Reading guide:
*Like other dot net apps, the app contains basic files and folders created conventionally*
[Whole folder here](https://github.com/ngocongduy/1888012-LTHDT-QLCH-WebAppNetCore/tree/master/1888012-LTHDT-QLCH-WebAppNetCore)
1. __Program.cs__: Firstly read when the app runs. A instance of web builder is created and it is going to read all our setting files:
	1. appsetting.json -> appsetting.{environment}.json
	1. usersecret.json *(Where to store our app secret - can be found in .csproj project-config file)*
	1. environment variables in Properties/launchsetting.json
	1. command-line argument (of the OS)
1. __appsettings.json and appsettings.Development.json__: Contain pre-configured settings for the app in both development and production environment
1. __Properties folder__: contains lauchSettings.json which help you to configure launching environment and properties for the app such as:
	- environmentVariables: Developement/Testing/Stagging/Production
	- applicationUrl
	- and so on ...
1. __Startup.cs__: After reading properties/settings in the files above, Running Environment (RE) will read this file to initialize services/middlewares for your app, most services call in this files using Dependency Injection pattern:
	- built-in or user-defined services to work with database
	- design pattern services: MVC
	- middlewares to control routing in your app: default routing pattern, authentication and authorization 
1. __wwwwroot__: this folder is your app local storage, you should call 'UseStaticFiles' middleware to work on this folder
1. __Controllers__: *Default names for MVC design pattern, should not change*. This folder should contains all controllers used in your app
1. __Models__: *Default names for MVC design pattern, should not change*. You define your models in here.
1. __ViewModels__: For models for views (made/extended from models) should be stored here.
1. __Views__: 
	- This folder contrain all views (razor pages) for the app. Views grouped by Controller-oriented. Such as sub-folder 'Account' will contains all views which can be accessed by action-based in the corresponding controller 'AccountController'. The framework will automatically works by default unless naming controller is not violated. 
	- Sub folder 'Shared' will be looked up if no views in the above routing rule found. *_Layout.cshtml* will help you create template for your Views
	- *_ViewImports.cshtml* & *_ViewStart.cshtml* will work with '_Layout.cshtml' to help you create template (common use code block) over all views.
1. __DAL__: Data access layer files shall be here.
1. __Migrations__: *Default folder generated by the framework*. This folder contains files to help you tracking database migration/update when using code-first database creation approach.
1. __Utilities__: Some additional/optional services can be made here.
1. __Security__: Some user-defined requirements/handlers for authorization and other kinds of security can be made here.  

**End!**
