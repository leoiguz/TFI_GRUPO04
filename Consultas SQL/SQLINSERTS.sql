use DBTIENDA;
--________________________________ INSERTAR NUMERO CORRELATIVO ________________________________
select * from NumeroCorrelativo
--000001
insert into NumeroCorrelativo(ultimoNumero,cantidadDigitos,gestion,fechaActualizacion) values
(0,6,'venta',getdate())

--________________________________ RECURSOS DE FIREBASE_STORAGE Y CORREO ________________________________
--(AQUI DEBES INCLUIR TUS PROPIAS CLAVES Y CRENDENCIALES)
select * from Configuracion;
insert into Configuracion(recurso,propiedad,valor) values
('Servicio_Correo','correo',''),
('Servicio_Correo','clave',''),
('Servicio_Correo','alias','MiTienda.com'),
('Servicio_Correo','host','smtp.gmail.com'),
('Servicio_Correo','puerto','587')

--delete Configuracion;

insert into Configuracion(recurso,propiedad,valor) values
('FireBase_Storage','email',''),
('FireBase_Storage','clave',''),
('FireBase_Storage','ruta',''),
('FireBase_Storage','api_key',''),
('FireBase_Storage','carpeta_usuario','IMAGENES_USUARIO'),
('FireBase_Storage','carpeta_producto','IMAGENES_PRODUCTO'),
('FireBase_Storage','carpeta_logo','IMAGENES_LOGO')

--___________ INSERTAR ROLES ___________
insert into rol(descripcion,esActivo) values
('Administrador',1),
('Vendedor',1),
('Supervisor',1)


--___________ INSERTAR USUARIO ___________
SELECT * FROM Usuario

--delete from Usuario where idUsuario =2; 
--clave : 123
insert into Usuario(nombre,correo,telefono,idRol,clave,esActivo) values
('codigo estudiante','codigo@example.com','909090',1,'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3',1)

--___________ INSERTAR SUCURSAL ___________

select * from Sucursal

insert into Sucursal(idSucursal,numeroDocumento,nombre,correo,domicilio,ciudad,telefono,porcentajeImpuesto,simboloMoneda)
values(1,'12346','centro','test@email.com','Tucuman','San Miguel','423564',2,'$')


--___________ INSERTAR CONDICION TRIBUTARIA ___________
insert into CondicionTributaria(nombre,esActivo) values
('RI',1),
('M',1),
('E',1),
('NR',1),
('CF',1)


--___________ INSERTAR MENUS ___________
--*menu padre
insert into Menu(descripcion,icono,controlador,paginaAccion,esActivo) values
('DashBoard','fas fa-fw fa-tachometer-alt','DashBoard','Index',1)

insert into Menu(descripcion,icono,esActivo) values
('Administración','fas fa-fw fa-cog',1),
('Inventario','fas fa-fw fa-clipboard-list',1),
('Ventas','fas fa-fw fa-tags',1),
('Reportes','fas fa-fw fa-chart-area',1)


--*menu hijos Administracion
insert into Menu(descripcion,idMenuPadre, controlador,paginaAccion,esActivo) values
('Usuarios',2,'Usuario','Index',1),
('Sucursal',2,'Sucursal','Index',1),
('Clientes',2,'Cliente','Index',1)


--*menu hijos - Inventario
insert into Menu(descripcion,idMenuPadre, controlador,paginaAccion,esActivo) values
('Articulos',3,'Articulo','Index',1),
('Colores',3,'Color','Index',1),
('Talles',3,'Talle','Index',1),
('TipoTalles',3,'TipoTalle','Index',1),
('Marcas',3,'Marca','Index',1),
('Categorias',3,'Categoria','Index',1),
('Inventarios',3,'Inventario','Index',1)

--*menu hijos - Ventas
insert into Menu(descripcion,idMenuPadre, controlador,paginaAccion,esActivo) values
('Nueva Venta',4,'Venta','NuevaVenta',1),
('Historial Venta',4,'Venta','HistorialVenta',1)

--*menu hijos - Reportes
insert into Menu(descripcion,idMenuPadre, controlador,paginaAccion,esActivo) values
('Reporte de Ventas',5,'Reporte','Index',1)


UPDATE Menu SET idMenuPadre = idMenu where idMenuPadre is null

--___________ INSERTAR ROL MENU ___________
--*administrador
INSERT INTO RolMenu(idRol,idMenu,esActivo) values
(1,1,1),
(1,6,1),
(1,7,1),
(1,8,1),
(1,9,1),
(1,10,1),
(1,11,1),
(1,12,1),
(1,13,1),
(1,14,1),
(1,15,1),
(1,16,1),
(1,17,1),
(1,18,1)

--*Vendedor
INSERT INTO RolMenu(idRol,idMenu,esActivo) values
(2,16,1),
(2,17,1)

--*Supervisor
INSERT INTO RolMenu(idRol,idMenu,esActivo) values
(3,8,1),
(3,9,1),
(3,10,1),
(3,11,1),
(3,12,1),
(3,13,1),
(3,14,1),
(3,15,1),
(3,16,1)




