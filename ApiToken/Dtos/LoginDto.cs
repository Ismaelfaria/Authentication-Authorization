namespace ApiToken.Dtos
{
    public record LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
