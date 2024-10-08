﻿using api_cinema_challenge.DTO;
using api_cinema_challenge.Models;
using api_cinema_challenge.Repository;
using api_cinema_challenge.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace api_cinema_challenge.EndPoints
{
    public static class MovieEndpoint
    {
        // Base path of the api call, used for the created call...
        private static string _path = AppContext.BaseDirectory;
        public static void ConfigureMovieEndpoint(this WebApplication app)
        {
            var movie = app.MapGroup("movies");

            movie.MapPost("/", CreateMovie);
            movie.MapGet("/", GetMovies);
            movie.MapPut("/{id}", UpdateMovie);
            movie.MapDelete("/{id}", DeleteMovie);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> CreateMovie(IRepository<Movie> repository, IRepository<Screening> screeningRepository, MovieView view)
        {
            DateTime creationTime = DateTime.UtcNow;
            var model = new Movie()
            { 
                Title = view.Title,
                Rating = view.Rating,
                Description = view.Description,
                RuntimeMins = view.RuntimeMins,
                CreatedAt = creationTime,
                UpdatedAt = creationTime
            };

            var result = await repository.Create([], model);
            var resultDTO = new MovieDTO(result);

            // Create any screenings that were sent in
            foreach (var screening in view.Screenings)
            {
                await ScreeningEndpoint.CreateScreening(screeningRepository, result.Id, screening);
            }

            var payload = new Payload<MovieDTO>() { Status = "success", Data = resultDTO };
            return TypedResults.Created(_path, payload);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetMovies(IRepository<Movie> repository)
        {
            var resultList = await repository.GetAll([]);
            var resultDTOs = new List<MovieDTO>();
            foreach (var result in resultList)
            {
                resultDTOs.Add(new MovieDTO(result));
            }

            var payload = new Payload<List<MovieDTO>>() { Status = "success", Data = resultDTOs };
            return TypedResults.Ok(payload);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> UpdateMovie(IRepository<Movie> repository, int id, MovieView view)
        {
            DateTime creationTime = DateTime.UtcNow;
            var model = new Movie()
            {
                Id = id,
                Title = view.Title,
                Rating = view.Rating,
                Description = view.Description,
                RuntimeMins = view.RuntimeMins,
                UpdatedAt = creationTime
            };
            var result = await repository.Update([], model);
            var resultDTO = new MovieDTO(result);

            var payload = new Payload<MovieDTO>() { Status = "success", Data = resultDTO };
            return TypedResults.Created(_path, payload);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> DeleteMovie(IRepository<Movie> repository, int id)
        {
            var result = await repository.Delete([], new Movie() { Id = id });
            var resultDTO = new MovieDTO(result);

            var payload = new Payload<MovieDTO>() { Status = "success", Data = resultDTO };
            return TypedResults.Ok(payload);
        }
    }
}