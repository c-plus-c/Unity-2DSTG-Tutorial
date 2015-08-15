using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Spaceship spaceship;

	// Use this for initialization
	IEnumerator Start () {

		spaceship = GetComponent<Spaceship> ();

		while (true) {
			spaceship.Shot(transform);
			GetComponent<AudioSource>().Play();
			yield return new WaitForSeconds(spaceship.shotDelay);
		}
	
	}

	void Move(Vector2 direction)
	{
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		Vector2 pos = transform.position;

		pos += direction  * spaceship.speed * Time.deltaTime;

		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);

		transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");

		Vector2 direction = new Vector2 (x, y).normalized;

		Move (direction);
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		// レイヤー名を取得
		string layerName = LayerMask.LayerToName(c.gameObject.layer);
		
		// レイヤー名がBullet (Enemy)の時は弾を削除
		if( layerName == "Bullet (Enemy)")
		{
			// 弾の削除
			Destroy(c.gameObject);
		}
		
		// レイヤー名がBullet (Enemy)またはEnemyの場合は爆発
		if( layerName == "Bullet (Enemy)" || layerName == "Enemy")
		{
			FindObjectOfType<Manager>().GameOver();

			// 爆発する
			spaceship.Explosion();
			
			// プレイヤーを削除
			Destroy (gameObject);
		}
	}
}
