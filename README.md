# O Pool de Objetos é um padrão de design de criação que pré-instancia todos os objetos que você precisará em qualquer momento específico antes do jogo. 
         
    Isso elimina a necessidade de criar novos objetos ou destruir os antigos enquanto o jogo está em execução.
    A coleta de lixo é uma técnica de gerenciamento automático de memória usada em linguagens de programação para recuperar a memória ocupada por objetos que não estão mais em uso pelo programa.
    O purColocar a coleta de lixo é liberar os desenvolvedores do gerenciamento manual de memória, reduzindo o risco de vazamentos de memória e ponteiros pendurados.

<br>

Declaro um Header como atributo utilizado para adicionar um cabeçalho personalizado na interface do Inspector

    [Header("Define qual o objeto que irá compor o pool")] 
    [SerializeField] private GameObject prefab;

Declaro uma variável privada chamada prefab do tipo GameObject que será usado para definir qual objeto será utilizado para formar o pool.

    [Header("Define a quantidade de objetos que serão criados")]
    [SerializeField] private int amountToPool;

Declaro uma variável privada chamada amountToPool do tipo int que define a quantidade de objetos que serão criados no pool

    private List<GameObject> pooledObjects = new();

Declaro uma lista chamada pooledObjects do tipo List<GameObject>. Essa lista será usada para armazenar os objetos que foram criados e desativados para reutilização.


    private void Start()
    {
        for (int index = 0; index < amountToPool; index++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

Nesse método Start, o loop for é usado para criar a quantidade definida de objetos no pool.
    
    A variável index é usada para controlar o número de objetos criados.
    Dentro do loop, um objeto é instanciado usando o Instantiate() e armazenado na variável obj.
    Em seguida, o objeto é desativado usando SetActive(false) e adicionado à lista pooledObjects para ser reutilizado.


    public GameObject GetPooledObject()
    {
        for (int index = 0; index < pooledObjects.Count; index++)
        {
            if (!pooledObjects[index].activeInHierarchy)
            {
                return pooledObjects[index];
            }
        }
        return null;
    }

O método GetPooledObject() é usado para obter um objeto do pool.
    
    A variável index é usada para controlar o número de objetos obtidos.
    Dentro do loop for percorre a lista de pooledObjects e verifico se algum objeto não está ativo na hierarquia.
    Se encontrar um objeto disponível, ele retorna esse objeto. Caso contrário, retorna null.


<br>

SpaceShip.cs

    private ObjectPooling shotsPooling;

Declaro uma variável shotsPooling como referência para o componente ObjectPooling, que será usado para obter os objetos de tiro do pool. 


    private void Start()
    {
        shotsPooling = GetComponent<ObjectPooling>();
    }


O método Start() referência o componente ObjectPooling que é obtida através do método GetComponent<ObjectPooling>().


    private void ShotFire()
    {
        canShotAgain = false;
        GameObject shot = shotsPooling.GetPooledObject();
        if (shot is null)
        {
            Debug.LogWarning("Sem tiros disponíveis");
            return;
        }
        shot.transform.position = shotPoint.position;
        shot.SetActive(true);
    }

O método ShotFire() é responsável por realizar o tiro. 
    
    Em seguida, chamo o método GetPooledObject() do componente referenciando o ObjectPooling para obter um objeto de tiro do pool. 
    Se não houver objetos disponíveis, ativa o objeto de tiro recuperado do pool, tornando-o visível e interagível no jogo.

<br>

Projectile.cs


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bateu");
        this.gameObject.SetActive(false);
        Destroy(other.gameObject);
    }

O método  é chamado quando ocorre uma colisão do projétil com outro objeto que possui um colisor.  
    
    Em seguida, o objeto do projétil é desativado usando  para que ele não seja mais visível ou interaja com outros objetos. 
    Além disso, o objeto que colidiu com o projétil () é destruído usando .OnTriggerEnter(Collider other)SetActive(false)other.gameObjectDestroy()



