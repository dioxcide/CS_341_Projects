#include <stdio.h>
#include <stdlib.h>
#include <deque>
#include <vector>

typedef struct Binary_Tree{

	int id;
	struct Binary_Tree * left;
	struct Binary_Tree * right;
}B_Tree;

typedef B_Tree* BT;
using namespace std;

// A utility function to create a node
B_Tree* newNode(int data)
{
	B_Tree* temp = (B_Tree *)malloc(sizeof(B_Tree));

	temp->id = data;
	temp->left = temp->right = NULL;

	return temp;
}

// A recursive function to construct Full from pre[] and post[]. 
// preIndex is used to keep track of index in pre[].
// l is low index and h is high index for the current subarray in post[]
B_Tree* constructTreeUtil(int pre[], int post[], int* preIndex, int l, int h, int size)
{
	// Base case
	if (*preIndex >= size || l > h)
		return NULL;

	// The first node in preorder traversal is root. So take the node at
	// preIndex from preorder and make it root, and increment preIndex
	B_Tree* root = newNode(pre[*preIndex]);
	++*preIndex;

	// If the current subarry has only one element, no need to recur
	if (l == h)
		return root;

	// Search the next element of pre[] in post[]
	int i;
	for (i = l; i <= h; ++i)
		if (pre[*preIndex] == post[i])
			break;

	// Use the index of element found in postorder to divide postorder array in
	// two parts. Left subtree and right subtree
	if (i <= h)
	{
		root->left = constructTreeUtil(pre, post, preIndex, l, i, size);
		root->right = constructTreeUtil(pre, post, preIndex, i + 1, h, size);
	}

	return root;
}

// The main function to construct Full Binary Tree from given preorder and 
// postorder traversals. This function mainly uses constructTreeUtil()
B_Tree *constructTree(int pre[], int post[], int size)
{
	int preIndex = 0;
	return constructTreeUtil(pre, post, &preIndex, 0, size - 1, size);
}

void BFS(B_Tree *start)
{
	deque<B_Tree *> q;
	q.push_back(start);
	while (q.size() != 0)
	{
		B_Tree *next = q.front();
		printf("%d, ", next->id);
		q.pop_front();
		if (next->left)
			q.push_back(next->left);
		if (next->right)
			q.push_back(next->right);
	}
}

void inOrder(B_Tree* trunk){
	if (trunk){
		inOrder(trunk->left);
		printf("ID: %d\n", trunk->id);
		inOrder(trunk->right);
	}
}

int main(int argc, char ** argv){
	B_Tree* trunk = NULL;
	FILE *pFile;
	vector <int> preorder;
	vector <int> postorder;
	
	int temp = 0;
	pFile = fopen("PracticeInput.txt", "r");
	bool newLine = false;

	while (fscanf(pFile, "%d%*c", &temp) != EOF){
		if (temp == '\n' && newLine == false){
			newLine = true;
		}
		else if (newLine == false){
			preorder.push_back(temp);
		}
		else if (newLine == true){
			postorder.push_back(temp);
		}
		
	}
	printf("Size: %d\n", preorder.size());
	printf("size2: %d\n", postorder.size());
	int *pre = preorder.data();
	int *post = postorder.data();
	trunk = constructTree(pre, post, postorder.size());

	BFS(trunk);
	return 0;
}