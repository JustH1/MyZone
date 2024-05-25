using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyZone.Structs
{
    public class reportDayOrder
    {
        public int dow { get; set; }
        public int count { get; set; }
    }
    public class catalogs
    {
        [Key]
        public int c_id { get; set; } 
        public string c_name { get; set; }
    }
    public class reviews
    {
        [Key]
        public int r_id { get; set; }
        public string r_name_author { get; set; }
        public int r_pr_id { get; set; }
        public string r_type { get; set; }
        public string r_advantages { get; set; }
        public string r_disadvantages { get; set; }
        public string r_comments { get; set; }
        public double r_rating { get; set; }
        public DateTime r_date_writing { get; set; }
    }
    public class basket
    {
        [Key]
        public int id { get; set; }
        [NotNull]
        public int b_u_id { get; set; }
        [NotNull]
        public int b_pr_id { get; set; }
        [NotNull]
        public decimal b_cost_product { get; set; }
        [NotNull]
        public string b_pr_name { get ; set; }
        [NotNull]
        public int b_sh_id { get; set; }
    }
    public class order_product
    {
        [Key]
        public int id { get; set; }        
        [NotNull]
        public ulong o_id { get; set; }
        [NotNull]
        public int pr_id { get; set; }
        public int quantity { get; set; }
    }
    public class order
    {
        [Key]
        public ulong o_id { get; set; }
        public decimal o_price { get; set; }
        public int o_u_id { get; set; }
        [MaxLength(50)]
        public string o_status { get; set; }
        public DateTime o_date_creation { get; set; }
        public DateTime? o_deli_date { get; set; }
        public int o_sh_id { get; set; }
    }
    [Keyless]
    public class order_pickuppoint
    {
        [NotNull]
        public ulong o_id { get; set; }
        [NotNull]
        public string pup_id { get; set; }
    }
    public class pickup_point
    {
        [Key]
        public int pop_id { get; set; }
        [MaxLength(255)]
        public string pup_description { get; set; }
        [MaxLength(255)]
        public string pup_address { get; set; }
        [MaxLength(255)]
        public string pup_email { get; set; }
        public int pup_count_reviews { get; set; }
        public double pup_reting { get; set; }
    }
    public class product
    {
        [Key]
        public int pr_id { get; set; }
        [MaxLength(50)]
        public string pr_name { get; set; }
        public string pr_description { get; set; }
        public string pr_characteristics { get; set; }
        public decimal pr_price { get; set; }
        public int pr_number_of_reviews { get; set; }
        public double pr_rating { get; set; }
        public int pr_shop_id { get; set; }
        public int catalog_id { get; set; }
    }
    public class shop
    {
        [Key]
        [NotNull]
        public int sh_id { get; set; }
        public string sh_name { get; set; }
        public DateTime? sh_date_creation { get; set; }
        public double sh_rating { get; set; }
        public string sh_email { get; set; }
        public string sh_phone_number { get; set; }
        public int sh_u_id { get; set; }
    }
    public class user_payment_method
    {
        [Key]
        [NotNull]
        public int u_id { get; set; }
        [NotNull]
        [MaxLength(19)]
        public string upm_cardnumber { get; set; }
    }
    public class users
    {
        [NotNull]
        [Key]
        public int u_id { get; set; }
        [NotNull]
        [MaxLength(50)]
        public string u_name { get; set; }
        [NotNull] 
        [MaxLength(255)]
        public string u_email { get; set; }
        [NotNull]
        [MaxLength(50)]
        public string u_rights { get; set; }
        [NotNull]
        public DateTime u_date_account_creation { get; set; }
        [NotNull]
        public string u_passwd { get; set; }
    }
}

