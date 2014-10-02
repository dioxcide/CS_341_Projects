#include <vector>

struct BTree{
	int id;
	BTree *left;
	BTree *right;
};

typedef BTree* T;

void addNode(T &trunk, int id){
	if (trunk == NULL){
		BTree* newNode = new BTree();
		newNode->id = id;
		newNode->left = NULL;
		newNode->right = NULL;
		trunk = newNode;
	}
	else if (id < trunk->id){
		addNode(trunk->left, id);
	}
	else if (id > trunk->id){
		addNode(trunk->right, id);
	}
}

void postTraversal(T trunk){
	postTraversal(trunk->left);
	printf("%d\n", trunk->id);
	postTraversal(trunk->right);
}

int main(int argc, char **argv){
	T trunk = new BTree();
	addNode(trunk, 10);
	addNode(trunk, 1);
	addNode(trunk, 25);
	addNode(trunk, 76);
	addNode(trunk, 23);
	addNode(trunk, 89);
	postTraversal(trunk);
}