using System;
using System.Collections.Generic;
using System.Linq;
using HangmanAPI.Data.Entity;
using HangmanAPI.Model.Entity;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;

namespace HangmanAPI.Model.Profile {
    public class GameProfile : AutoMapper.Profile {
        public GameProfile() {
            CreateMap<Game, GameModel>()
                .ForMember(dest => dest.Guesses, opt => opt.MapFrom(x => x.Guesses))
                .ForMember(dest => dest.Word, opt => opt.MapFrom(x => x.Word));
        }
    }
}
