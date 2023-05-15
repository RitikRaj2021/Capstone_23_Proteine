namespace Capstone_23_Proteine.Models.Domain
{
    public class FoodIntake
    {
        public Guid ID { get; set; }
        public int Protein { get; set; }
        public int Calories { get; set; }
        public int Fat { get; set; }
        public string MealName { get; set; }
        public DateTime Date { get; set; }
    }
}
