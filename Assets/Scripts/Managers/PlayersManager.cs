using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    public GameManager gm;

    [Header("Records List")]
    public List<List<RecordFrame>> replays;

    [Header("Players and ghosts")]
    public List<GameObject> players;
    public List<GameObject> ghosts;

    [Header("Players and ghost Prefabs")]
    public List<GameObject> characters;
    public GameObject ghost;

    [Header("Lists ANimator Controllers")]
    public List<RuntimeAnimatorController> player1_anim;
    public List<RuntimeAnimatorController> player2_anim;
    public List<RuntimeAnimatorController> ghostAnim;

    [Header("Weapon Prefabs")]
    public List<GameObject> weapons;
    public GameObject bullet;
    public GameObject grenadeBullet;

    [Header("Game Properties")]
    public int player1Score;                //Player 1 score in current game
    public int player2Score;                //Player 2 score in current game
    public int playerWonRound;
    public int rounds=1;                      //Current round
    public int maxRounds=5;                   //Maximum rounds 

    private void Start()
    {
        replays = new List<List<RecordFrame>>();
        players = new List<GameObject>();
        ghosts = new List<GameObject>();
        InstantiatePlayers();
    }

    //Instantiating 2 players from prefabs
    void InstantiatePlayers()
    {
        for (int i = 0; i <2; i++)
        {
            GameObject go = Instantiate(characters[i].gameObject, transform);
            //Setting player index 
            go.GetComponent<PlayerData>().playerIndex = i;

            //Adding player to players list
            players.Add(go);
        }
    }

    //Instantiate ghosts
    void InstantiateGhost()
    {
        foreach (List<RecordFrame> replay in replays)
        {
            GameObject go = Instantiate(ghost, transform);
            ghosts.Add(go);
            go.GetComponent<DataReplay>().record = replay;
        }
    }

    //Adding weapon to player
    public void AddWeaponToPlayer(int playerIndex, int weaponIndex, bool hasWeapon)
    {
        //Checking if player already has weapon
        players[playerIndex].gameObject.BroadcastMessage("DestroyWeapon",SendMessageOptions.DontRequireReceiver);
        players[playerIndex].GetComponent<PlayerData>().hasWeapon = false;

        //Instantiating weapon to player
        GameObject go = Instantiate(weapons[weaponIndex], players[playerIndex].transform);

        //Pushing bullet prefab
        if (weaponIndex == 1)
        {
            go.gameObject.GetComponent<Gun>().AddBullet(bullet);
        }
        else if (weaponIndex == 2)
        {
            go.gameObject.GetComponent<Shotgun>().AddBullet(bullet);
        }
        else if (weaponIndex==3)
        {
            go.gameObject.GetComponent<Grenade>().AddGrenadeBullet(grenadeBullet);
        }
           
        //Setting has weapon and weapon index
        players[playerIndex].GetComponent<PlayerData>().hasWeapon = true;
        players[playerIndex].GetComponent<PlayerData>().weaponIndex = weaponIndex;

        //Calling for changing animator function
        ChangeAnimator(playerIndex, weaponIndex);
    }

    //Adding weapont to ghost
    public void AddWeaponGhost(GameObject ghost, int weaponIndex)
    {
        ghost.gameObject.BroadcastMessage("DestroyWeapon", SendMessageOptions.DontRequireReceiver);
        ghost.GetComponent<GhostData>().hasWeapon = false;

        //Instantiating weapon to player
        GameObject go = Instantiate(weapons[weaponIndex], ghost.transform);

        //Pushing bullet prefab
        if (weaponIndex == 1)
        {
            go.gameObject.GetComponent<Gun>().AddBullet(bullet);
        }
        else if (weaponIndex == 2)
        {
            go.gameObject.GetComponent<Shotgun>().AddBullet(bullet);
        }
        else if (weaponIndex == 3)
        {
            go.gameObject.GetComponent<Grenade>().AddGrenadeBullet(grenadeBullet);
        }

        ghost.GetComponent<GhostData>().hasWeapon = true;
        ghost.GetComponent<GhostData>().weaponIndex = weaponIndex;
        ChangeGhostAnimator(ghost, weaponIndex);//to do
    }

    //Changing Animator on current player
    void ChangeAnimator(int playerIndex, int weaponIndex)
    {
        //Setting animator with player index and current weapon
        if(playerIndex==0)
            players[0].GetComponent<Animator>().runtimeAnimatorController = player1_anim[weaponIndex];
        else if(playerIndex == 1)
            players[1].GetComponent<Animator>().runtimeAnimatorController = player2_anim[weaponIndex];
    }

    //Changing Animator on ghost
    void ChangeGhostAnimator(GameObject ghost, int weaponIndex)
    {
        ghost.GetComponent<Animator>().runtimeAnimatorController = ghostAnim[weaponIndex];
    }

    public void EndRound()
    {
        FreezeAll();

        //Deleting all bullets and grenades
        DestroyAllBullet();

        AddScore();

        if (rounds != maxRounds)
        {
            DeletePlayersGhosts();
            Invoke("RestartRound", 4f);

            rounds++;
            gm.ChangeRound();
        }
        else if(rounds==maxRounds)
        {
            GameOver();
        }
        
    }

    //Finishing the game
    void GameOver()
    {
        if (player1Score > player2Score)
        {
            gm.GameOver(0);
        }
        else if(player2Score > player1Score)
        {
            gm.GameOver(1);
        }
        else if (player1Score == player2Score)
        {
            gm.GameOver(2);
        }
    }

    void DeletePlayersGhosts()
    {
        //Deleting every ghost in this scene
        foreach (GameObject ghosta in ghosts)
        {
            Destroy(ghosta);
        }

        //Clearing ghosts list
        ghosts.Clear();

        //Pushing records to Replay list
        replays.Add(players[0].GetComponent<DataRecorder>().record);
        replays.Add(players[1].GetComponent<DataRecorder>().record);

        //Deleting every player
        for (int i = players.Count - 1; i >= 0; i--)
        {
            Destroy(players[i].gameObject);
        }

        //CLearing players list
        players.Clear();

    }

    //Restarting round
    void RestartRound()
    {
        InstantiatePlayers();
        InstantiateGhost();
    }


    //Destroyng all bullets and grenades on scene
    void DestroyAllBullet()
    {
        //Creating array with all bullets
        GameObject[] bullets;
        bullets = GameObject.FindGameObjectsWithTag("Bullet");

        //Using for loop deleting every bullet
        for (var i = bullets.Length -1; i >=0 ; i--)
        {
            Destroy(bullets[i]);
        }
    }

    //Freezing all players and ghost when player died
    void FreezeAll()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        for (int j = 0; j < ghosts.Count; j++)
        { 
            if(ghosts[j]!=null)
            ghosts[j].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    //Adding score for players
    void AddScore()
    {
        if(players[0].GetComponent<PlayerData>().isDead)
        {
            player2Score++;
            playerWonRound = 1;
            gm.AddCrown(1);
        }
        else
        {
            player1Score++;
            playerWonRound = 0;
            gm.AddCrown(0);
        }
    }    
}
