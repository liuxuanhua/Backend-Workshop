using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MyMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MyMvc.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: Movies/Index
        public ActionResult Index(string movieGenre, string searchString)
        {
            var genreLst = new List<string>();

            var genreQry = from d in db.Movies
                           orderby d.Genre
                           select d.Genre;

            genreLst.AddRange(genreQry.Distinct());
            ViewBag.MovieGenre = new SelectList(genreLst);

            // # homework 3 -- read movies data from loacl-db,please use linq



            // # homework 7 -- filte movies data by conditions

            IQueryable<Movie> searchQuery = db.Movies;
            if (searchString != null && searchString.Trim().Length > 0)
            {
                searchQuery.Where(o => o.Title.Contains(searchString));
            }
            if (movieGenre != null && movieGenre.Trim().Length > 0)
            {
                searchQuery.Where(o => o.Rating.Equals(movieGenre));
            }
            var movies = searchQuery.OrderBy(o => o.Genre).ToList();
            ViewData["data"] = movies;

            return View();
        }

        [HttpPost]
        public string Index(FormCollection fc, string searchString)
        {
            return "<h3> From [HttpPost]Index: " + searchString + "</h3>";
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                StatusCodeResult result = new StatusCodeResult((int)HttpStatusCode.BadRequest);
                return result;
            }

            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return new NotFoundResult();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ID,Title,ReleaseDate,Genre,Price,Rating")]
            Movie movie)
        {
            // # homework 5 -- save data to loacl-db
            db.Movies.Add(movie);          
            db.SaveChanges();

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            // # homework 8 -- when you on Eidt site , you should see the movie info
            if (id == null)
            {
                Movie movie = db.Movies.Find(id);
                if(movie != null)
                {
                    return View(movie);
                }
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }

            return View();
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ID,Title,ReleaseDate,Genre,Price,Rating")]
            Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            // # homework 9 -- find data by id 
            // when id is null ,return HttpStatusCode.BadRequest;
            if (id == null)
            {
                Movie movie = db.Movies.Find(id);
                if (movie == null)
                {
                    return new StatusCodeResult((int)HttpStatusCode.BadRequest);
                }
                db.Movies.Remove(movie);
                db.SaveChanges();
            }
            return View();
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}