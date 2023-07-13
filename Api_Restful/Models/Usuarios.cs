using System.ComponentModel.DataAnnotations;

namespace Api_Restful.Models;

public partial class Usuarios
{
    public int IdUsuario { get; set; }

    [Required(ErrorMessage = "O nome deve ser inserido")]
    [MinLength(3, ErrorMessage = "O nome deve conter no mínimo 3 caracteres")]
    [MaxLength(80, ErrorMessage = "O nome deve conter no máximo 80 caracteres")]
    [RegularExpression(@"^[ a-zA-Z á]*$", ErrorMessage = "O nome deve conter apenas letras.")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "A senha deve ser inserida")]
    [MinLength(6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres")]
    [MaxLength(80, ErrorMessage = "A senha deve conter no máximo 80 caracteres")]
    public string Senha { get; set; } = null!;

    [Required(ErrorMessage = "O email deve ser inserido")]
    [MinLength(10, ErrorMessage = "O email deve conter no mínimo 10 caracteres")]
    [MaxLength(150, ErrorMessage = "O email deve conter no máximo 80 caracteres")]
    [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "O Email é inválido, insira outro.")]
    public string Email { get; set; } = null!;
}
