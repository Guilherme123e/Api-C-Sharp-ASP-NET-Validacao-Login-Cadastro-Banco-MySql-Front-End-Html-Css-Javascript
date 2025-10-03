using Microsoft.AspNetCore.Mvc;
using LoginApi.Data;
using LoginApi.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace LoginApi.Controllers
{
  [ApiController]
  [Route("api/usuarios")]
  public class UsuariosController : ControllerBase
  {
    private readonly AppDbContext _context;

    public UsuariosController(AppDbContext context)
    {
      _context = context;
    }

    [HttpPost("cadastrar")]
    public async Task<IActionResult> Cadastrar([FromBody] Usuario usuario)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState); // Retorna erros de validação detalhados

      if (await _context.Usuarios.AnyAsync(u => u.Cpf == usuario.Cpf))
        return BadRequest(new { campo = "cpf", mensagem = "CPF já cadastrado." });

      if (await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email))
        return BadRequest(new { campo = "email", mensagem = "Email já cadastrado." });

      // Garante que a senha em texto puro está no campo senha_hash
      usuario.Senha_hash = BCrypt.Net.BCrypt.HashPassword(usuario.Senha_hash);
      _context.Usuarios.Add(usuario);
      await _context.SaveChangesAsync();

      return Ok(new { sucesso = true });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest login)
    {
      var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == login.Email);
      if (usuario == null || !BCrypt.Net.BCrypt.Verify(login.Senha, usuario.Senha_hash))
        return BadRequest(new { mensagem = "Email ou senha inválidos." });

      return Ok(new { sucesso = true });
    }
  }
}
