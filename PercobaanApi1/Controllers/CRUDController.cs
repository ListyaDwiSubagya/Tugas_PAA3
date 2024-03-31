using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PercobaanApi1.Models;

namespace PercobaanApi1.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CRUDController : Controller
    {
        private string __constr;
        public CRUDController(IConfiguration configuration)
        {
            __constr = configuration.GetConnectionString("WebApiDatabase");
        }

        public IActionResult index()
        {
            return View();
        }

        [HttpGet("api/murid/read data")]

        public ActionResult<Murid> ListMurid()
        {
            MuridContext context = new MuridContext(this.__constr);
            List<Murid> ListDataMurid = context.ListDataMurid();
            return Ok(ListDataMurid);
        }

        [HttpPost("api/murid/create data")]
        public IActionResult CreatePerson([FromBody] Murid murid)
        {
            MuridContext context = new MuridContext(this.__constr);
            context.AddDataMurid(murid);
            return Ok("Data berhasil di tambahkan");
        }

        [HttpPut("api/murid/update data/{id}")]
        public IActionResult UpdatePerson(int id, [FromBody] Murid murid)
        {
            murid.id_murid = id;
            MuridContext context = new MuridContext(this.__constr);
            context.UpdateDataMurid(murid);
            return Ok("Data berhasil di update");
        }

        [HttpDelete("api/murid/delete data/{id}")]
        public IActionResult DeletePerson(int id)
        {
            MuridContext context = new MuridContext(this.__constr);
            context.DeleteDataMurid(id);
            return Ok("Data berhasil di hapus");
        }

    }

}

