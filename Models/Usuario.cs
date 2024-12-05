using System.ComponentModel.DataAnnotations;

namespace TCC_BR.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Insira um email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O celular é obrigatório")]
        [Phone(ErrorMessage = "Insira um número de celular válido")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        //[RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$", ErrorMessage = "Insira um CPF válido")]
        public string ? CPF { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "A senha e a confirmação devem ser iguais")]
        public string ? Confirme { get; set; }

        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "Selecione uma opção")]
        [StringLength(1, ErrorMessage = "Deve conter 1 caracter")]
        public string Sexo { get; set; }

        [Display(Name = "Nascimento")]
        [Required(ErrorMessage = "A data é obrigatorio")]
        public DateTime Nascimento { get; set; }

        [Display(Name = "Situação")]
        public string? Situacao { get; set; }


        /*Endereço residencial do cliente*/

        [Display(Name = "CEP", Description = "CEP.")]
        [MaxLength(8, ErrorMessage = "O CEP deve ter 8 digitos")]
        [MinLength(8, ErrorMessage = "O CEP deve ter 8 digitos")]
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public string CEP { get; set; }

        [Display(Name = "Estado", Description = "Estado.")]
        [Required(ErrorMessage = "O Estado é obrigatório.")]
        public string Estado { get; set; }

        [Display(Name = "Cidade", Description = "Cidade.")]
        [Required(ErrorMessage = "A Cidade é obrigatório.")]
        public string Cidade { get; set; }

        [Display(Name = "Bairro", Description = "Bairro.")]
        [Required(ErrorMessage = "O Bairro é obrigatório.")]
        public string Bairro { get; set; }

        [Display(Name = "Endereco", Description = "Endereco.")]
        [Required(ErrorMessage = "O Endereco é obrigatório.")]
        public string Endereco { get; set; }

        [Display(Name = "Complemento", Description = "Complemento.")]
        [Required(ErrorMessage = "O Complemento é obrigatório.")]
        public string Complemento { get; set; }

        [Display(Name = "Número", Description = "Complemento.")]
        public string Numero { get; set; }

    }

}
