using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyZone.Structs
{
    public class catalogs
    {
        [Key]
        public int c_id { get; set; } 
        public string c_name { get; set; }
        public int c_count { get; set; }
    }
    public class reviews
    {
        [Key]
        public int r_id { get; set; }
        public string r_name_author { get; set; }
        public int r_item_id { get; set; }
        public int r_type { get; set; }
        public string r_advantages { get; set; }
        public string r_disadvantages { get; set; }
        public string r_comments { get; set; }
        public double r_rating { get; set; }
        public string r_date_writing { get; set; }
    }
    public class user_order
    {
        [NotNull]
        public int u_id { get; set; }
        [NotNull]
        public int o_id { get; set; }
    }
    public class order_shop
    {
        [NotNull]
        public int o_id { get; set; }
        [NotNull]
        public string sh_id { get; set; }
    }
    public class order_product
    {
        [NotNull]
        public int r_id { get; set; }
        [NotNull]
        public string pr_id { get; set; }
    }
    public class order
    {
        [Key]
        public int o_id { get; set; }
        public double o_price { get; set; }
        public int catalog_id { get; set; }
        [MaxLength(50)]
        public string o_date_creation { get; set; }
    }
    public class order_pickuppoint
    {
        [NotNull]
        public int o_id { get; set; }
        [NotNull]
        public string pup_id { get; set; }
    }
    public class order_delivery_status
    {
        [Key]
        public int o_id { get; set; }
        [MaxLength(50)]
        public string deli_status { get; set; }
        public int deli_day { get; set; }
        [MaxLength(50)]
        public string deli_ocd { get; set; }
        [MaxLength(50)]
        public string deli_odd { get; set; }
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
        public double pr_price { get; set; }
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
        public string sh_date_creation { get; set; }
        public double sh_rating { get; set; }
        public string sh_email { get; set; }
        public string sh_phone_number { get; set; }
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
        [System.ComponentModel.DataAnnotations.Key]
        public int u_id { get; set; }
        [NotNull]
        [MaxLength(50)]
        public string u_name { get; set; }
        [NotNull] 
        [MaxLength(255)]
        public string u_email { get; set; }
        [NotNull]
        [MaxLength(255)]
        public string u_phone_number { get; set; }
        [NotNull]
        [MaxLength(50)]
        public string u_rights { get; set; }
        [NotNull]
        public string u_date_account_creation { get; set; }
    }
}

