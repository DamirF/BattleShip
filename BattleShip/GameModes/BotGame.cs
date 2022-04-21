using BattleShip.bot;
using System;
using System.Collections.Generic;
using System.Text;


namespace BattleShip.GameModes
{
    
    internal class BotGame : Game
    {
        private List<Ship> playerShips;
        private BOT bot;

        public BotGame(List<Ship> ships)
        {
            playerShips = ships;
            bot = new BOT(playerShips);
        }

    }
}
