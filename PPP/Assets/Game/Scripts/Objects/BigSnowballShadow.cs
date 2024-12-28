using UnityEngine;

public class BigSnowballShadow : MonoBehaviour
{
    private AudioManager _audioManager => AudioManager.I;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Penguin_Controller.I.SetHasPopsicle(false);

        if (collision.GetComponent<IDamageable>() != null)
        {
            collision.GetComponent<IDamageable>().TakeDamage(2);
        }

        _audioManager.PlaySfx("bigsnowballhit");
    }
}
