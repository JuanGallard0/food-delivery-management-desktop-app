# JHGH_00091813_2EP
Repositorio segundo exámen parcial POO

Link de trello: https://trello.com/b/rPazqpYs/ex%C3%A1men-parcial-2

P.S: Pequeño percance con el script.	

No sé cómo no me di cuenta de que el script de la base de datos estaba adjunto con las indicaciones del parcial, entonces hice mi propio script (dbScript.txt) viendo el diagrama.

Como sql parece ser case insensitive, mi app funciona bien con ambos script; las únicas diferencias que encontré son que el password VARCHAR(25) debe ser más grande en el script brindado porque usé el encriptador de la claseGUI5, y que mi script no tiene CASCADE en la llaves foráneas. 
Hay que ingresar un usuario con password encriptado para poder entrar:

--Username: 'admin'		password: 'admin'

INSERT INTO appuser (username, password, usertype, fullname)
VALUES ('admin', '21232f297a57a5a743894a0e4a801fc3', true, 'admin');