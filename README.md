# 1888012-LTHDT-QLCH-WebAppNetCore
A basic web app with net core 3.0

### Preface: 
This is my first web app made during learning. Most of the work of my app are originated from the internet, especially thanks to the tutorial for ASP NET CORE of sir Kuvenkat - PragimTech.
I publish it just for personal use during learning. You are free to use if it helps. 

### Design architecture: MVC & 3-layer
I. #### MVC
- Controller: control routes within the app, performing main works of the app
- View: Razor pages responded to clients. Each view may use one or more ViewModel for DataBinding when clients send requests to the server and vice versa.
- Model: a collection of Model (for general uses) and ViewModel (for View)

I. #### 3-layer
- Presentation layer (PL): This are plain pages sent by server and rendered by a browser on clients. It performs client-side works (using Javascript and relating library) such as data validation.
If required, it will send requests to server on behalf of the user. Then PL mainly work with Views.
(Upon a client's request, server will pick a corresponding view, do some required works (thanks to a cooperation in M-V-C) then generate a html-page, finally send it to client for rendering.)
- Business layer (BL): This layer is a hidden working environment in the server, the playground for Models, Views and Controllers. It is where main operations done: routing inside the app, authorization, authentications, data queries and so on.
This layer is the intermediate controlling data transactions between clients and databases.
- Data access layer (DL): The layer standing some models to help you interect with databases. In this case is SQL server. You can also define models to work with your local storages.


### App description: This web app is a very simple Store Management which can help the users (store staff) track products in the store.
1. #### View (Presentation layer)
  	1. Login (default route) & Register: In order to join, you must register for an account and log in
	1. Home
		+ Homepage: Index page give you 2 options for generating reports: StockByType & OutOfDate
		+ Thong ke theo loai: StockByType
		+ Thong ke theo HSD: OutOfDate
	1. Product: you can add/ update/ delete/ and search for product type and product by routing among this category
		+ Them/ Cap nhat/ Xoa/ Tim kiem san pham: Detail
		+ Tim kiem/ Cap nhat/ Xoa san pham: Search
		+ THem/ Cap nhat/ Xoa/ Tim Kiem loai san pham: ProductType
		+ Tim kiem/ Cap nhat/ Xoa loai san pham: SearchType
	1. Stock: you can add/ update/ delete/ and search for records of products in and out the Store
		+ Them/ Cap nhat/ Xoa/ Tim kiem phieu nhap: StockIn
		+ Them/ Cap nhat/ Xoa/ Tim kiem don hang: StockOut 
#### 1. Model & ViewModel (Business layer and Presentation layer)
	1. Model:
		+ Lop san pham: Product
		+ Lop loai san phan: ProductType
		+ Lop phieu nhap va don hang: StockTrackDetail
		+ Giao thuc: IProductRepositor __( For Constructor Dependency Injection - Only for ProductController)__
		+ Lop thuc thi: MockProductRepository 
	1. ViewModel: cac lop du lieu trung gian de tuong tac giua Controller va View
		+ Dung cho cac View lien quan den san pham: ProductViewModel
		+ Dung cho cac View lien quan den loai san pham: ProductTypeViewModel
		+ Dung cho cac View lien quan den phieu nhap: StockInViewModel
		+ Dung cho cac View lien quan den ban hang: StockOutViewModel
#### 1. Controller (Business layer)
	1. Dieu khien cac View lien quan den san pham va loai san pham: ProductController
	1. Dieu khien cac View lien quan den phieu nhap va don hang: StockController
	1 Dieu khien cac View lien quan den trang chu va thong ke: HomeController

#### 1. Data accesslayer (DAL): to read and write files in folder root/data
	1. Du lieu cua san pham: product.json
	1. Du lieu cua loai san pham: product_type.json
	1. Du lieu phieu nhap: stock_in.json
	1. Du lieu don hang: stock_out.json
