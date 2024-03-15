Feature: Solicitar Autorización

  Scenario: Solicitar autorización con un código válido
    Given que tengo un código de autorización válido "4E6C1831-0C85-439F-AB55-02D4227CE970"
    When envío una solicitud para solicitar autorización con el código "4E6C1831-0C85-439F-AB55-02D4227CE970"
    Then debería recibir un código de autorización válido

  Scenario: Solicitar autorización con un código inválido
    Given que tengo un código de autorización inválido "codigo_invalido"
    When envío una solicitud para solicitar autorización con el código "codigo_invalido"
    Then debería recibir un mensaje de error