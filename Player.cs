using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! Write comments fool !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = .5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;


    // Start is called before the first frame update
    void Start()
    {
        // Starting position: (0,0,0)
        transform.position = new Vector3(3, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            fireLaser();
        }
    }
    void CalculateMovement()
    {

        // Initialized user input to horizontal axis (see project settings)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // new Vector3(3.5,1,0) * UI right: 1 * 3.5f * realtime
        // transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        // transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        // Optimized version of above code
        // transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        // More optimized version of above code
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        /*if (transform.position.y >= 7) // Player moves to 7
        {
            transform.position = new Vector3(transform.position.x, 7, 0); // Stop and stay at same position on x axis
        }
        if (transform.position.y <= -5.5f)
        {
            transform.position = new Vector3(transform.position.x, -5.5f, 0);
        }*/
        // Optimized version of above code
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5.5f, 7), 0);

        if (transform.position.x >= 10.5f)
        {
            transform.position = new Vector3(-10.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -10.5f)
        {
            transform.position = new Vector3(10.5f, transform.position.y, 0);
        }
    }

    void fireLaser()
    {
        // private float _fireRate = .5f;
        // private float _canFire = -1f;
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(-0.2f, 0.8f, 0), Quaternion.identity);
        Instantiate(_laserPrefab, transform.position + new Vector3(0.2f, 0.8f, 0), Quaternion.identity);
    }
    public void Damage()
    {
        _lives -= 1;
        if (_lives < 1)
        {
            Destroy(this.gameObject);
        }
    }
}

/* Key
transform.position = current position
new Vector3() = new position

*/