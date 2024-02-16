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

select * from Sucursal

insert into Sucursal(idSucursal,numeroDocumento,nombre,correo,domicilio,ciudad,telefono,porcentajeImpuesto,simboloMoneda)
values(1,'12346','centro','test@email.com','Tucuman','San Miguel','423564',2,'$')

insert into CondicionTributaria(nombre,esActivo) values
('RI',1),
('M',1),
('E',1),
('NR',1),
('CF',1)
