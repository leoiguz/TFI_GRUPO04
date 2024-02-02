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
('Servicio_Correo','correo','micaelrodriguez68@gmail.com'),
('Servicio_Correo','clave','chusebgzwzsjzsdb'),
('Servicio_Correo','alias','MiTienda.com'),
('Servicio_Correo','host','smtp.gmail.com'),
('Servicio_Correo','puerto','587')

insert into Configuracion(recurso,propiedad,valor) values
('FireBase_Storage','email','micael@gmail.com'),
('FireBase_Storage','clave','micael'),
('FireBase_Storage','ruta','mitiendaonline-2f5c4.appspot.com'),
('FireBase_Storage','api_key','AIzaSyBpX85iE38mI88ykWbLcYYt_6HzuN_gYFU'),
('FireBase_Storage','carpeta_usuario','IMAGENES_USUARIO'),
('FireBase_Storage','carpeta_producto','IMAGENES_PRODUCTO'),
('FireBase_Storage','carpeta_logo','IMAGENES_LOGO')
