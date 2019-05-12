using UnityEngine;


public class PlayerData : MonoBehaviour
{
    PlayerAnimationController animController;
    PlayerController playerCont;

    [Header("Status Flags")]
    public bool isDead;             //Is player dead?
    public int playerIndex;         //Player index 0 or 1
    public bool hasWeapon;          //Is player has weapon?
    public int weaponIndex;         //Weapon index which player had

    private void Start()
    {
        animController=GetComponent<PlayerAnimationController>();
        playerCont=GetComponent<PlayerController>();
    }

    public void Die()
    {
        //Check if this player already was killed
        if (!isDead)
        {
            //Seting isDead to true
            isDead = true;
            // Sending trigger for die animation
            animController.SetPlayerDead();
            //Calling end round function in manager
            transform.parent.GetComponent<PlayersManager>().EndRound();
            //Calling for die flying in controller
            playerCont.Die();
        }
    }
}
