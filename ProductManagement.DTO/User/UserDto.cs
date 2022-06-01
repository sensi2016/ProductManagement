namespace ProductManagement.DTO.User
{
    public class DashboardUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string? Password { get; set; }
        public string UserName { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public string ImageProfile { get; set; }
        public string? GrandFatherName { get; set; }
        public string? GreatGrandFatherName { get; set; }
        //public string? MotherFatherName { get; set; }
        //public string? MotherGrandFatherName { get; set; }
        public string? NationalId { get; set; }
        public string? PersonalCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? RoleId { get; set; }
        public int? DegressId { get; set; }
        public int? CityId { get; set; }
        public bool IsActive { get; set; } 

    }

    public class FilterDashboardUserDto:IPaging
    {
        public string FullName { get; set; }
        public int? RoleId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
