using System.Collections.Generic;
using Chromia;
using Chromia.Encoding;
using Dc.Online;

namespace Dc.ChrSerializable
{

    // Translation of the Rell struct in_game_result.
    [PostchainSerializable]
    public class ChrGameData
    {
        [PostchainProperty("game_rowid")]
        public long GameRowid; // game_rowid: integer;
        [PostchainProperty("game_type")]
        public OnlineGameType GameType;  // game_type: game_type;
        [PostchainProperty("is_white")]
        public bool IsWhite; // is_white: boolean;
        [PostchainProperty("opponent_pubkey")]
        public Chromia.Buffer OppPubKey; // opponent_pubkey: pubkey;
        [PostchainProperty("opponent_name")]
        public string OppName; // opponent_name: text;
        [PostchainProperty("opponent_elo")]
        public long OppELO; // opponent_elo: integer;
        [PostchainProperty("curr_turn_nr")]
        public long CurrentTurnNumber; // curr_turn_nr: integer; // If the game just started, this will be 0
        [PostchainProperty("created_at")]
        public long CreatedAt; // created_at: timestamp; // When the game was created

    }
}
