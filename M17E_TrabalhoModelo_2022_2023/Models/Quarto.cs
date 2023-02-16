using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M17E_TrabalhoModelo_2022_2023.Models
{
    public class Quarto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Tem de indicar o piso do quarto (de 1 a 5)")]
        [Range(1,5,ErrorMessage ="O piso tem de estar entre 1 e 5")]
        [UIHint("Insira o piso do quarto (de 1 a 5)")]
        public int Piso { get; set; }
        
        [Required(ErrorMessage ="Tem de indicar a lotação máxima do quarto (de 1 a 5)")]
        [Range(1,5,ErrorMessage ="O quarto tem de ter uma lotação entre 1 e 5")]
        [UIHint("Insira a lotação do quarto (de 1 a 5)")]
        [Display(Name ="Lotação")]
        public int Lotacao { get; set; }

        [Required(ErrorMessage = "Tem de indicar o custo do quarto")]
        [Range(0, 1000, ErrorMessage = "Custo do quarto deve estar entre 0 e 1000")]
        [UIHint("Indique o custo do quarto por dia")]
        [Display(Name = "Custo por dia")]
        [DataType(DataType.Currency)]
        public decimal Custo_dia { get; set; }
        [Display(Name = "Casa de banho")]
        public bool Casa_banho { get; set; }
        public bool Estado { get; set; }
        [Required(ErrorMessage ="Tem de indicar o tipo de quarto")]
        [StringLength(20)]
        [MinLength(3,ErrorMessage ="O tipo de quarto deve ter pelo menos 3 letras")]
        [Display(Name ="Tipo de Quarto")]
        public string Tipo_Quarto { get; set; }

        public Quarto()
        {
            Estado = true;
        }
    }
}