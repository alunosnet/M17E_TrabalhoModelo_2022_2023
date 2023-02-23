using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M17E_TrabalhoModelo_2022_2023.Models
{
    public class Utilizador
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Indique um nome de utilizador")]
        public string Nome { get; set; }
        [Required(ErrorMessage ="Indique um email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage ="Indique uma palavra passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage ="Indique o perfil do utilizador")]
        public int Perfil { get; set; }
        [Display(Name ="Estado da conta")]
        public bool Estado { get; set; }
        //dropdown perfil
        public IEnumerable<System.Web.Mvc.SelectListItem> Perfis { get; set; }
    }
}