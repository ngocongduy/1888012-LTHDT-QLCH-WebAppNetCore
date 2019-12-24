WebAppNetCore

Thiet ke: Mo hinh MVC + 3-layer
I. View: Presentation layer
	1. Home:
		+ Trang chu: Index
		+ Thong ke theo loai: StockByType
		+ Thong ke theo HSD: OutOfDate
	2. Product:
		+ Them/ Cap nhat/ Xoa/ Tim kiem san pham: Detail
		+ Tim kiem/ Cap nhat/ Xoa san pham: Search
		+ THem/ Cap nhat/ Xoa/ Tim Kiem loai san pham: ProductType
		+ Tim kiem/ Cap nhat/ Xoa loai san pham: SearchType
	3. Stock:
		+ Them/ Cap nhat/ Xoa/ Tim kiem phieu nhap: StockIn
		+ Them/ Cap nhat/ Xoa/ Tim kiem don hang: StockOut
II. Model & ViewModel: Kieu du lieu cho Business layer va Presentation layer
	1. Model:
		+ Lop san pham: Product
		+ Lop loai san phan: ProductType
		+ Lop phieu nhap va don hang: StockTrackDetail
		+ Giao thuc: IProductRepository (*)
		+ Lop thuc thi: MockProductRepository (*)
		(*) Su dung cho Constructor Dependency Injection - Chi dung cho ProductController
	2. ViewModel: cac lop du lieu trung gian de tuong tac giua Controller va View
		+ Dung cho cac View lien quan den san pham: ProductViewModel
		+ Dung cho cac View lien quan den loai san pham: ProductTypeViewModel
		+ Dung cho cac View lien quan den phieu nhap: StockInViewModel
		+ Dung cho cac View lien quan den ban hang: StockOutViewModel
III. Controller: Business layer
	1. Dieu khien cac View lien quan den san pham va loai san pham: ProductController
	2. Dieu khien cac View lien quan den phieu nhap va don hang: StockController
	3. Dieu khien cac View lien quan den trang chu va thong ke: HomeController

IV. Data accesslayer (DAL): dung de doc ghi du lieu vao cac file trong thu muc root/data
	1. Du lieu cua san pham: product.json
	2. Du lieu cua loai san pham: product_type.json
	3. Du lieu phieu nhap: stock_in.json
	4. Du lieu don hang: stock_out.json

