Feature: Editar Color
  Como Administrador
  Quiero poder editar un color existente
  Para actualizar su información

  Scenario: Editar un color existente correctamente
    Given que tengo un color con la siguiente información:
      | Id  | Descripcion  |
      | 1   | Azul         |
    When realizo una solicitud para editar el color con la siguiente información:
      | Id  | Descripcion |
      | 1   | Celeste     |
    Then el color editado tiene la siguiente información:
      | Id  | Descripcion |
      | 1   | Celeste     | 
