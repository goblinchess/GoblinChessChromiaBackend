using System.Collections.Generic;
using Chromia;
using Chromia.Encoding;
using Dc.Online;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    // Translation of the Rell struct timeout_data.
    [PostchainSerializable]
    public class ChrTimeoutIData
    {
        [PostchainProperty("opp_timeout")]
        public bool OppTimeout; // True if the opponent has timed out

        [PostchainProperty("opp_last_heartbeat")]
        public long OppLastHeartbeat; // When was the opponent last active?

        [PostchainProperty("my_time_left_ms")]
        public long MyTimeLeftMs; // Amount of time I have for my next move

        [PostchainProperty("opp_time_left_ms")]
        public long OppTimeLeftMs; // Amount of time the opponent has for this move

        [PostchainProperty("curr_time_ms")]
        public long CurrTimeMs; // The time the node sees

        [PostchainProperty("last_color_rotation_at")]
        public long LastColorRotationAt; // When the last color rotation was

        [PostchainProperty("game_started_at")]
        public long GameStartedAt; // When the game was started
    }

    public class X {

                // This holds one entire row (= stats for one old game)
        public Transform Layout_OldGamesTable; // This is the parent object for the "old games" table rows, i.e. the "table".
        public GameObject Prefab_RowOldGame; // Dev must set this in Unity GUI

        private void _fillRowWithData() {

        }
    }



}
