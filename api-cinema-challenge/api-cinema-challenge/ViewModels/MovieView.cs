﻿namespace api_cinema_challenge.ViewModels
{
    public class MovieView
    {
        public string Title { get; set; }
        public string Rating { get; set; }
        public string Description { get; set; }
        public int RuntimeMins { get; set; }
        public List<ScreeningView> Screenings { get; set; }
    }
}
